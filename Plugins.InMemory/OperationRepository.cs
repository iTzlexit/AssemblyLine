using AssemblyLine.ApplicationLayer.DTO;
using AssemblyLine.ApplicationLayer.PluginInterfaces;
using Entities;
using System;


namespace Plugins.InMemory
{
    /// <summary>
    ///         //The await Task.CompletedTask is used to simulate an asynchronous operation, which is useful if you plan to replace the in-memory operation with an actual asynchronous database operation in the future.This keeps the method signature asynchronous.

    /// </summary>
    public class OperationRepository : IOperationRepository
    {

        public async Task<IEnumerable<OperationResponse>> GetOperationsAsync(int? assemblyId)
        {
            var operations = new List<Operation>();

            if (assemblyId == 0)
            {
                // Get the min assembly ID
                var maxAssemblyId = MockDb.DbOperations.Min(a => a.AssemblyId);
                // Fetch operations for the assembly with the maximum ID
                operations = MockDb.DbOperations
                                    .Where(x => x.AssemblyId == maxAssemblyId)
                                    .OrderBy(o => o.OrderInWhichToPerform)
                                    .ToList();

                if (operations.Count < 0)
                {
                     throw new InvalidOperationException("There are no operations for the given assembly");
                }
            }
            else
            {
                // Fetch operations for the specified assembly ID
                operations = MockDb.DbOperations
                                    .Where(x => x.AssemblyId == assemblyId)
                                    .OrderBy(o => o.OrderInWhichToPerform)
                                    .ToList();

                if (operations.Count < 0)
                {
                    throw new InvalidOperationException("There are no operations for the given assembly");
                }
            }

            var response = operations.Select(operation => new OperationResponse
            {
                OperationId = operation.OperationId,
                Name = operation.OperationName,
                OrderInWhichToPerform = operation.OrderInWhichToPerform,
                ImageData = operation.ImageData,
                DeviceName = MockDb.DbDevices.FirstOrDefault(d => d.DeviceId == operation.DeviceId)?.Name ?? "",
                DeviceType = MockDb.DbDevices.FirstOrDefault(d => d.DeviceId == operation.DeviceId)?.DeviceType.ToString() ?? ""
            }).ToList();

            return await Task.FromResult(response);
        }


        /// <summary>
        /// It searches for an Operation with the specified operationId.
        ///If the operation is found, it saves the name of the operation, removes the operation from the list, and then returns the operation's name.
        /// </summary>
        public async Task<OperationResponse> DeleteOperationAsync(int operationId)
        {
            var operation = MockDb.DbOperations.FirstOrDefault(x => x.OperationId == operationId);

            if (operation != null)
            {
                MockDb.DbOperations.Remove(operation);
                return await Task.FromResult(new OperationResponse { Name = operation.OperationName  });
            }


            return await Task.FromResult<OperationResponse>(null);
        }

    

        public async Task<OperationResponse> AddOperationAsync(OperationAddRequest operationRequest)
        {
            if (operationRequest == null)
            {
                return null; // or an appropriate response indicating the request is invalid
            }

            bool exists = await ExistingOrderToPerform(operationRequest.OrderInWhichToPerform ?? 0, operationRequest.AssemblyId);
            if (exists)
            {
                // Returning an appropriate response indicating the order already exists
                throw new InvalidOperationException("An operation with the specified order already exists for this assembly. Please choose a different order number.");
            }

            bool isAllowed = await IsAllowedOrder(operationRequest.OrderInWhichToPerform ?? 0, operationRequest.AssemblyId);
            if (!isAllowed)
            {
                throw new InvalidOperationException("An operation with the specified order already exists for this assembly. Please choose a different order number.");
            }


            operationRequest.ImageData = operationRequest.ImageData ?? new byte[0];

            var maxId = MockDb.DbOperations.Any() ? MockDb.DbOperations.Max(x => x.OperationId) : 0;
            operationRequest.Id = maxId + 1;

            var operationEntity = operationRequest.ToOperationEntity();
            MockDb.DbOperations.Add(operationEntity);

            return new OperationResponse
            {
                OperationId = operationEntity.OperationId,
                Name = operationEntity.OperationName
            };
        }

        //Valiation Check 
        public async Task<bool> ExistingOrderToPerform(int orderToPerform, int assemblyId)
        {
            return await Task.FromResult(MockDb.DbOperations
           .Any(x => x.AssemblyId == assemblyId && x.OrderInWhichToPerform == orderToPerform));
        }

        //Validation Check - checing the order is the next sequential number , 

        public async Task<bool> IsAllowedOrder(int orderToPerform, int assemblyId)
        {
            var existingOrders = MockDb.DbOperations
                                        .Where(x => x.AssemblyId == assemblyId)
                                        .Select(x => x.OrderInWhichToPerform)
                                        .OrderBy(x => x)
                                        .ToList();

            if (existingOrders.Contains(orderToPerform))
            {
                // If the order number already exists, return false
                return await Task.FromResult(false);
            }

            int nextAvailableOrder = existingOrders.Any() ? existingOrders.Last() + 1 : 1;

            // Allow adding an order number if it's within the range of existing numbers or the next available number
            return await Task.FromResult(orderToPerform <= nextAvailableOrder);
        }



    }
}

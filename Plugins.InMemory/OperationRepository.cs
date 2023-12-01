using AssemblyLine.ApplicationLayer.DTO;
using AssemblyLine.ApplicationLayer.PluginInterfaces;
using Entities;
using System;
using System.Reflection;


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
                AssemblyId = operation.AssemblyId,
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

            // First check: If there are no operations, the first operation must start at order to perform 1
            bool startOrderCheck = StartOrderNumber(operationRequest.OrderInWhichToPerform ?? 0, operationRequest.AssemblyId);
            if (!startOrderCheck)
            {
                throw new InvalidOperationException("The first operation for an assembly must start at order number 1.");
            }

            // Second check: If the order already exists
            bool exists = await ExistingOrderToPerform(operationRequest.OrderInWhichToPerform ?? 0, operationRequest.AssemblyId);
            if (exists)
            {
                throw new InvalidOperationException("An operation with the specified order already exists for this assembly. Please choose a different order number.");
            }

            // Third check: If the order is allowed (considering gaps and sequence)
            bool isAllowed = IsAllowedOrder(operationRequest.OrderInWhichToPerform ?? 0, operationRequest.AssemblyId);
            if (!isAllowed)
            {
                throw new InvalidOperationException("The order number is not valid. Please choose a different order number or follow the sequence of orders to perform .");
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


        public bool StartOrderNumber(int orderToPerform, int assemblyId)
        {
            var existingOrders = MockDb.DbOperations
                                        .Where(x => x.AssemblyId == assemblyId)
                                        .Select(x => x.OrderInWhichToPerform)
                                        .ToList();

            // If there are no existing orders, check if the order number is 1
            if (!existingOrders.Any())
            {
                // Return true only if orderToPerform is 1
                return orderToPerform == 1;
            }

            // If there are existing orders, this validation is not applicable, return true
            return true;
        }




        public bool IsAllowedOrder(int orderToPerform, int assemblyId)
        {
            var existingOrders = MockDb.DbOperations
                                        .Where(x => x.AssemblyId == assemblyId)
                                        .Select(x => x.OrderInWhichToPerform)
                                        .OrderBy(x => x)
                                        .ToList();

            // If there are no existing orders, only allow order number 1
            if (!existingOrders.Any())
            {
                return true;
            }

            // If the order number already exists, return false
            if (existingOrders.Contains(orderToPerform))
            {
                return false;
            }

            // Determine the maximum order number currently in use
            int maxOrder = existingOrders.Max();

            // Check for gaps in the sequence and allow if the order number is within the gap
            for (int i = 1; i <= maxOrder; i++)
            {
                if (!existingOrders.Contains(i))
                {
                    if (i == orderToPerform)
                    {
                        return true;
                    }
                }
            }

            // Allow adding an order number if it's exactly one more than the current maximum
            return orderToPerform == maxOrder + 1;
        }





        public async Task <int?> GetDefaultOrderOperation(int assemblyId)
        {
            var assemblies = MockDb.DbAssemblies;


            if (assemblyId == 0)
            {
                return null; 
            }

            var operationsCount = MockDb.DbOperations.Where(x => x.AssemblyId == assemblyId).Count();

            return await Task.FromResult(operationsCount + 1);
        }

    }
}

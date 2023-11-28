using ApplicationLayer.DTO;
using ApplicationLayer.Operations;
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
       

        public async Task <IEnumerable<OperationResponse>> GetOperationsAsync()
        {
            IEnumerable<Operation> operations = MockDb.DbOperations;

            // coverting to DTO
            IEnumerable<OperationResponse> response = operations.ToOperationResponse();


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

            bool exists = await ExistingOrderToPerform(operationRequest.OrderInWhichToPerform, operationRequest.AssemblyId);
            if (exists)
            {
                // Returning an appropriate response indicating the order already exists
                throw new InvalidOperationException("An operation with the specified order already exists for this assembly. Please choose a different order number.");
            }

            bool isSequential = await IsSequentialOrder(operationRequest.OrderInWhichToPerform, operationRequest.AssemblyId);
            if (!isSequential)
            {
                throw new InvalidOperationException("The order number must follow the sequence of existing operations. Please choose the next sequential order number.");
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
        public async Task<bool> IsSequentialOrder(int orderToPerform, int assemblyId)
        {
            int? maxOrder = MockDb.DbOperations
                                  .Where(x => x.AssemblyId == assemblyId)
                                  .Max(x => (int?)x.OrderInWhichToPerform);

            // return true If no operations exist for this assembly, allow starting from order 1
            if (!maxOrder.HasValue)
            {
                return await Task.FromResult(orderToPerform == 1);
            }

            // return true if the provided order is the next sequential number if its any other order besides the previous one it returns false 
            return await Task.FromResult(orderToPerform == maxOrder.Value + 1); 
        }


    }
}

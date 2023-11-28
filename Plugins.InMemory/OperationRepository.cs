using ApplicationLayer.DTO;
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
       

        public Task <IEnumerable<OperationResponse>> GetOperationsAsync()
        {
            IEnumerable<Operation> operations = MockDb.DbOperations;

            // coverting to DTO
            IEnumerable<OperationResponse> response = operations.ToOperationResponse();


            return Task.FromResult(response);
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
                return await Task.FromResult(new OperationResponse { Name = operation.Name });
            }


            return await Task.FromResult<OperationResponse>(null);
        }
    }
}

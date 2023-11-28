using ApplicationLayer.DTO;
using AssemblyLine.ApplicationLayer.PluginInterfaces;
using Entities;


namespace Plugins.InMemory
{
    public class OperationRepository : IOperationRepository
    {
       

        public Task <IEnumerable<OperationResponse>> GetOperationsAsync()
        {
            IEnumerable<Operation> operations = MockDb.Operations;

            // coverting to DTO
            IEnumerable<OperationResponse> response = operations.ToOperationResponse();


            return Task.FromResult(response);
        }
    }
}

using AssemblyLine.ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.PluginInterfaces
{
    public interface IOperationRepository
    {
        Task<IEnumerable<OperationResponse>> GetOperationsAsync(int? assemblyId);
        Task<OperationResponse> DeleteOperationAsync(int operationId);

        Task<OperationResponse> AddOperationAsync(OperationAddRequest operationRequest);

    }
}
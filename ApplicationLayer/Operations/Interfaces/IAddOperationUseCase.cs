using AssemblyLine.ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.Operations.Interfaces
{
    public interface IAddOperationUseCase
    {
        Task<OperationResponse> ExecuteAsync(OperationAddRequest operationAddRequest);
    }
}
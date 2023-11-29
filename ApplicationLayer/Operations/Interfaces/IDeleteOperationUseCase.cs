using AssemblyLine.ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.Operations.Interfaces
{
    public interface IDeleteOperationUseCase
    {
        Task<OperationResponse> ExecuteAsync(int operationId);
    }
}
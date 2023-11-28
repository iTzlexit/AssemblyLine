using ApplicationLayer.DTO;

namespace ApplicationLayer.Operations.Interfaces
{
    public interface IDeleteOperationUseCase
    {
        Task<OperationResponse> ExecuteAsync(int operationId);
    }
}
using ApplicationLayer.DTO;

namespace ApplicationLayer.Operations.Interfaces
{
    public interface IGetListOfOperationsUseCase
    {
        Task<IEnumerable<OperationResponse>> ExecuteAsync();
    }
}
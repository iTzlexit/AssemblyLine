using AssemblyLine.ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.Operations.Interfaces
{
    public interface IGetListOfOperationsUseCase
    {
        Task<IEnumerable<OperationResponse>> ExecuteAsync();
    }
}
using AssemblyLine.ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.Assembly.Interface
{
    public interface IFetchAssembliesUseCase
    {
        Task<IEnumerable<AssemblyResponse>> ExecuteAsync(); 
    }
}
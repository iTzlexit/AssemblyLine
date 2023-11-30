using AssemblyLine.ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.Assembly.Interface
{
    public interface IFetchAssemblyWithDefaultOrderNumber
    {
        Task<IEnumerable<AssemblyResponse>> ExecuteAsync(int assemblyId); 
    }
}
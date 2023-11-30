using AssemblyLine.ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.PluginInterfaces
{
    public interface IAssemblyRepository
    {
         Task<IEnumerable<AssemblyResponse>> GetListOfAssemblies();
        Task<IEnumerable<AssemblyResponse>> GetListOfAssemblieswithorderToPerformOperations(int assemblyId = 0); 
    }
}
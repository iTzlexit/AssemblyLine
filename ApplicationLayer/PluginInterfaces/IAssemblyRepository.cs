using AssemblyLine.ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.PluginInterfaces
{
    public interface IAssemblyRepository
    {
         Task<IEnumerable<AssemblyResponse>> GetListOfAssemblies(); 
    }
}
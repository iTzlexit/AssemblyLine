using AssemblyLine.ApplicationLayer.DTO;
using AssemblyLine.ApplicationLayer.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.InMemory
{
    public class AssemblyRepository: IAssemblyRepository
    {
        public async Task<IEnumerable<AssemblyResponse>> GetListOfAssemblies()
        {
            var Assemblies = MockDb.DbAssemblies;



            if(Assemblies == null)
            {

                return await Task.FromResult(new List<AssemblyResponse>());
            }
            else
            {
                var response = Assemblies.ToAssemblyResponse();
                return await Task.FromResult(response);
            }

        }
    }
}

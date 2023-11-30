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

            if (Assemblies == null)
            {
                return await Task.FromResult(new List<AssemblyResponse>());
            }
            

            var response = Assemblies.ToAssemblyResponse();
            return await Task.FromResult(response);
        }


        public async Task<IEnumerable<AssemblyResponse>> GetListOfAssemblieswithorderToPerformOperations(int assemblyId = 0)
        {
            var assemblies = MockDb.DbAssemblies;

            if (assemblies == null)
            {
                return await Task.FromResult(new List<AssemblyResponse>());
            }

            if (assemblyId > 0)
            {
                assemblies = assemblies.Where(a => a.Id == assemblyId).ToList();
            }

            var response = new List<AssemblyResponse>();
            foreach (var assembly in assemblies)
            {
                // Ensure there's a list of operations and find the max OrderInWhichToPerform
                var maxOrder = assembly.operations.Any() ? assembly.operations.Max(op => op.OrderInWhichToPerform) : 0;

                var assemblyResponse = new AssemblyResponse
                {
                    Id = assembly.Id,
                    AssemblyName = assembly.AssemblyName,
                    DefaultOrderNumber = maxOrder + 1, // Set the default order number
                };

                response.Add(assemblyResponse);
            }

            return await Task.FromResult(response);
        }

    }
}

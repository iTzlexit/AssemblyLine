
using AssemblyLine.ApplicationLayer.Assembly.Interface;
using AssemblyLine.ApplicationLayer.DTO;
using AssemblyLine.ApplicationLayer.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Assembly
{
    public class FetchAssemblyWithDefaultOrderNumber: IFetchAssemblyWithDefaultOrderNumber
    {
        private readonly IAssemblyRepository _assemblyRepository;

        public FetchAssemblyWithDefaultOrderNumber(IAssemblyRepository assemblyRepository)
        {
            _assemblyRepository = assemblyRepository;
        }

        public async Task<IEnumerable<AssemblyResponse>> ExecuteAsync(int assemblyId)
        {
            return await _assemblyRepository.GetListOfAssemblieswithorderToPerformOperations(assemblyId);
        }
    }
}

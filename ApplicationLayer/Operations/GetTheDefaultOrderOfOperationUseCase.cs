using AssemblyLine.ApplicationLayer.Operations.Interfaces;
using AssemblyLine.ApplicationLayer.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Operations
{
    public class GetTheDefaultOrderOfOperationUseCase : IGetTheDefaultOrderOfOperationUseCase
    {
        private readonly IOperationRepository _operationRepository;

        public GetTheDefaultOrderOfOperationUseCase(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task<int?> ExecuteAsyc(int assemblyId)
        {

            return await _operationRepository.GetDefaultOrderOperation(assemblyId); 
        }
    }
}

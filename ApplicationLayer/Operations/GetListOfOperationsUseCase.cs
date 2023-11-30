using AssemblyLine.ApplicationLayer.DTO;

using AssemblyLine.ApplicationLayer.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLine.ApplicationLayer.Operations.Interfaces
{
    public class GetListOfOperationsUseCase : IGetListOfOperationsUseCase
    {
        private readonly IOperationRepository _operationRepository;

        public GetListOfOperationsUseCase( IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }


        public async Task<IEnumerable<OperationResponse>> ExecuteAsync( int? assemblyId)
        {
            var response = await _operationRepository.GetOperationsAsync( assemblyId);

            return response;    
        }
    }
}

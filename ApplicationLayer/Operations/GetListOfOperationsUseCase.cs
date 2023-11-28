using ApplicationLayer.DTO;
using ApplicationLayer.Operations.Interfaces;
using AssemblyLine.ApplicationLayer.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Operations
{
    public class GetListOfOperationsUseCase : IGetListOfOperationsUseCase
    {
        private readonly IOperationRepository _operationRepository;

        public GetListOfOperationsUseCase( IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }


        public async Task<IEnumerable<OperationResponse>> ExecuteAsync()
        {
            return await _operationRepository.GetOperationsAsync();
        }
    }
}

using AssemblyLine.ApplicationLayer.DTO;

using AssemblyLine.ApplicationLayer.PluginInterfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLine.ApplicationLayer.Operations.Interfaces
{
    public class AddOperationUseCase: IAddOperationUseCase
    {
        private readonly IOperationRepository _operationRepository;

        public AddOperationUseCase(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task<OperationResponse> ExecuteAsync (OperationAddRequest operationAddRequest)
        {
            var response = await _operationRepository.AddOperationAsync (operationAddRequest);

            if(response != null)
            {
                return response;
            }
            else
            {
                throw new InvalidOperationException($"Operation could not be added.");
            }
        }
    }
}

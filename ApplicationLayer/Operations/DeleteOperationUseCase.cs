using AssemblyLine.ApplicationLayer.DTO;

using AssemblyLine.ApplicationLayer.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLine.ApplicationLayer.Operations.Interfaces
{
    public class DeleteOperationUseCase : IDeleteOperationUseCase
    {
        private readonly IOperationRepository _operationRepository;

        public DeleteOperationUseCase(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task<OperationResponse> ExecuteAsync (int operationId)
        {
          var operationResponse = await _operationRepository.DeleteOperationAsync (operationId);

            if (operationResponse != null)
            {
                return operationResponse;
            }
            else
            {
                throw new InvalidOperationException($"Operation with ID {operationId} could not be found or deleted.");
            }
        }
    }
}

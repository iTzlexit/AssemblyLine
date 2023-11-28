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

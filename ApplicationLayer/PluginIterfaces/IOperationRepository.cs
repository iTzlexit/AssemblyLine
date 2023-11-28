﻿using ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.PluginInterfaces
{
    public interface IOperationRepository
    {
        Task<IEnumerable<OperationResponse>> GetOperationsAsync();
        Task<OperationResponse> DeleteOperationAsync(int operationId); 

    }
}
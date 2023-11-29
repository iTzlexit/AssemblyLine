using ApplicationLayer.Devices.Interfaces;
using AssemblyLine.ApplicationLayer.DTO;
using AssemblyLine.ApplicationLayer.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLine.ApplicationLayer.Devices.Interfaces
{
    public class FetchDeviceOperationandAssemblyUseCase: IFetchDeviceOperationandAssemblyUseCase
    {
        private readonly IDeviceRepository _deviceRepository;

        public FetchDeviceOperationandAssemblyUseCase(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<AssemblyOperationResponseForDevice> FetchDeviceModalContent(int selectedAssembly) 
        {
            var response = await _deviceRepository.GetDeviceModalContent(selectedAssembly);

            return response;
        }
    }
}

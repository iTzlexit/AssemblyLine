using ApplicationLayer.DTO;
using AssemblyLine.ApplicationLayer.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Devices
{
    public class FetchDeviceOperationandAssemblyUseCase
    {
        private readonly IDeviceRepository _deviceRepository;

        public FetchDeviceOperationandAssemblyUseCase(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<AssemblyOperationResponseForDevice> FetchDeviceModalContent(int selectedAssembly) 
        {
            return await _deviceRepository.GetDeviceModalContent(selectedAssembly);
        }
    }
}

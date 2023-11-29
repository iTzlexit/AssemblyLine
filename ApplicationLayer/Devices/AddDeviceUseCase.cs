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
    public class AddDeviceUseCase: IAddDeviceUseCase
    {
        private readonly IDeviceRepository _deviceRepository;

        public AddDeviceUseCase(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<DeviceResponse> ExecuteAsync(DeviceAddRequest deviceAddRequest)
        {
            var response = await _deviceRepository.AddDeviceAsync(deviceAddRequest);

            if (response != null)
            {
                return response;
            }
            else
            {
                throw new InvalidOperationException("Device could not be added"); 
            }
        }
    }
}

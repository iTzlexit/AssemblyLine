using AssemblyLine.ApplicationLayer.Devices.Interfaces;
using AssemblyLine.ApplicationLayer.DTO;
using AssemblyLine.ApplicationLayer.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLine.ApplicationLayer.Devices
{
    public class FetchDeviceTypes: IFetchDeviceTypes
    {
        private readonly IDeviceRepository _deviceRepository;

        public FetchDeviceTypes(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<IEnumerable<ResponseForDeviceTypes>> Executeasync() 
        {
            var response = await _deviceRepository.GetDeviceTypesForModal();

            return response;
        }
    }
}

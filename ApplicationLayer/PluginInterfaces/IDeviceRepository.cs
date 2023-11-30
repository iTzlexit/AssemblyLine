using AssemblyLine.ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.PluginInterfaces
{
    public interface IDeviceRepository
    {
        Task<DeviceResponse> AddDeviceAsync(DeviceAddRequest deviceAddRequest);
        Task<IEnumerable<ResponseForDeviceTypes>> GetDeviceTypesForModal(); 
    }
}
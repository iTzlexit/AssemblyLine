using AssemblyLine.ApplicationLayer.DTO;

namespace ApplicationLayer.Devices.Interfaces
{
    public interface IAddDeviceUseCase
    {
        Task<DeviceResponse> ExecuteAsync(DeviceAddRequest deviceAddRequest); 
    }
}
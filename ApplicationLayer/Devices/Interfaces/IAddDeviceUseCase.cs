using AssemblyLine.ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.Devices.Interfaces
{
    public interface IAddDeviceUseCase
    {
        Task<DeviceResponse> ExecuteAsync(DeviceAddRequest deviceAddRequest); 
    }
}
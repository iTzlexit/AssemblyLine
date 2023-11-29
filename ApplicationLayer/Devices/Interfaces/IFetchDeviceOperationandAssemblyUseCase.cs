using AssemblyLine.ApplicationLayer.DTO;

namespace ApplicationLayer.Devices.Interfaces
{
    public interface IFetchDeviceOperationandAssemblyUseCase
    {
        Task<AssemblyOperationResponseForDevice> FetchDeviceModalContent(int selectedAssembly);
    }
}
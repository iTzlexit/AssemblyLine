using AssemblyLine.ApplicationLayer.DTO;

namespace AssemblyLine.ApplicationLayer.Devices.Interfaces
{
    public interface IFetchDeviceTypes
    {
        Task<IEnumerable<ResponseForDeviceTypes>> Executeasync(); 
    }
}
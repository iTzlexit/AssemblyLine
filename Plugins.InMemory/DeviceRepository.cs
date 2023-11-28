using ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyLine.ApplicationLayer.PluginInterfaces;

namespace Plugins.InMemory
{
    public class DeviceRepository: IDeviceRepository
    {
       
        public async Task<DeviceResponse> AddDeviceAsync( DeviceAddRequest deviceAddRequest)
        {
            if(deviceAddRequest == null)
            {
                return await Task.FromResult<DeviceResponse>(null);
            }

            var maxId = MockDb.DbDevices.Any() ? MockDb.DbDevices.Max(x => x.DeviceId) : 0; 

            deviceAddRequest.Id = maxId +1;



            var toDeviceEntity = deviceAddRequest.ToDeviceEntity();
            MockDb.DbDevices.Add(toDeviceEntity);

            if (deviceAddRequest.OperationId > 0)
            {
                bool assignmentSuccess = await AssignDeviceToOperation(deviceAddRequest.OperationId, deviceAddRequest.Id, deviceAddRequest.AssemblyId);
                if (!assignmentSuccess) // if true
                {
                    throw new InvalidOperationException("WAs unable to assign a device to the operation");
                }
            }


            return await Task.FromResult<DeviceResponse>( new DeviceResponse { DeviceId = toDeviceEntity.DeviceId, Name = toDeviceEntity.Name});

        }

       
        ///  assigns a device to an operation that belongs to a particular assembly, if returns false  /
       
        public async Task<bool> AssignDeviceToOperation(int operationId, int deviceId, int assemblyId)
        {
            var operation = MockDb.DbOperations.FirstOrDefault(o => o.OperationId == operationId);

            if (operation == null || operation.AssemblyId != assemblyId) // if it belongs to a specific assembly 
            {
                // Operation not found or does not belong to the selected assembly
                return await Task.FromResult(false);
            }

            if (operation.DeviceId != 0)
            {
                // Operation already has a device assigned
                return await Task.FromResult(false);
            }

            operation.DeviceId = deviceId;
            return await Task.FromResult(true);
        }





    }
}

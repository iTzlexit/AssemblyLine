using AssemblyLine.ApplicationLayer.DTO;
using AssemblyLine.ApplicationLayer.PluginInterfaces;
using Entities;
using System.Collections;

namespace Plugins.InMemory
{
    public class DeviceRepository: IDeviceRepository
    {
       
        public async Task<DeviceResponse> AddDeviceAsync(DeviceAddRequest deviceAddRequest)
        {
            if(deviceAddRequest == null)
            {
                return await Task.FromResult<DeviceResponse>(null);
            }

            var maxId = MockDb.DbDevices.Any() ? MockDb.DbDevices.Max(x => x.DeviceId) : 0; 

            deviceAddRequest.Id = maxId +1;


            if (deviceAddRequest.OperationId > 0)
            {
                bool assignmentSuccess = await AssignDeviceToAssemblyOperation(deviceAddRequest.OperationId, deviceAddRequest.Id, deviceAddRequest.AssemblyId);
                if (!assignmentSuccess) // if true
                {
                    throw new InvalidOperationException("Was unable to assign a device to the operation");
                }

                bool deviceExistsInAssembly = await DeviceAlreadyExists(deviceAddRequest.OperationId, deviceAddRequest.DeviceTypeId, deviceAddRequest.AssemblyId); 

                if(deviceExistsInAssembly)
                {

                    throw new InvalidOperationException("You cant assign the same device type to the same assembly line, assign a new device"); 
                }

            }
            var toDeviceEntity = deviceAddRequest.ToDeviceEntity();
            MockDb.DbDevices.Add(toDeviceEntity);


            return await Task.FromResult<DeviceResponse>( new DeviceResponse { DeviceId = toDeviceEntity.DeviceId, Name = toDeviceEntity.Name});

        }


        // if device type already exists for a particular assembly then return an error 
        public async Task<bool> DeviceAlreadyExists(int operationId, int deviceTypeId, int assemblyId)
        {
            // Retrieve all operations associated with the given assembly
            var operationsInAssembly = MockDb.DbOperations.Where(x => x.AssemblyId == assemblyId);

            // Iterate through the operations to check if any of them has a device with the same type
            foreach (var operation in operationsInAssembly)
            {
                // Check if the current operation is not the one we are trying to add the device to
                if (operation.OperationId != operationId)
                {
                    // Retrieve the device associated with this operation
                    var device = MockDb.DbDevices.FirstOrDefault(x => x.DeviceId == operation.DeviceId);

                    // Check if the device exists and its type matches the requested type
                    if (device != null && device.DeviceType == (DeviceType)deviceTypeId)
                    {
                        // Found a device with the same type in the same assembly
                        return await Task.FromResult(true);
                    }
                }
            }

            // No device of the same type found in the same assembly
            return await Task.FromResult(false);
        }





        ///  assigns a device to an operation that belongs to a particular assembly, if returns false  /

        public async Task<bool> AssignDeviceToAssemblyOperation(int operationId, int deviceId, int assemblyId)
        {
            var operation = MockDb.DbOperations.FirstOrDefault(o => o.OperationId == operationId);

            if (operation == null || operation.AssemblyId != assemblyId) // if it belongs to a specific assembly 
            {
                // Operation not found or does not belong to the selected assembly
                return await Task.FromResult(false);
            }

            // Check if the operation already has a different device assigned
            if (operation.DeviceId != 0 && operation.DeviceId != deviceId)
            {
                // An existing device is already assigned to this operation
                return await Task.FromResult(false);
            }

            operation.DeviceId = deviceId;
            return await Task.FromResult(true);
        }



        //----------------------Fetching the assemblies and operations for add device modal and populate the operation based on assembly ---------//

        public async Task<IEnumerable<ResponseForDeviceTypes>> GetDeviceTypesForModal()
        {
         
            var deviceTypes = Enum.GetValues(typeof(DeviceType))
                .Cast<DeviceType>()
                .Select(v => new ResponseForDeviceTypes
                {
                    ID = (int)v,
                    Name = v.ToString()
                })
                .ToList();

            // Return the deviceTypes
            return await Task.FromResult(deviceTypes);

        }


    }
}

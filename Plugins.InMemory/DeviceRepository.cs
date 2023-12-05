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
            if (deviceAddRequest == null)
            {
                return await Task.FromResult<DeviceResponse>(null);
            }

            var maxId = MockDb.DbDevices.Any() ? MockDb.DbDevices.Max(x => x.DeviceId) : 0;
            deviceAddRequest.Id = maxId + 1;

            if (deviceAddRequest.OperationId > 0)
            {
                var operation = MockDb.DbOperations.FirstOrDefault(o => o.OperationId == deviceAddRequest.OperationId);
                if (operation != null)
                {
                    // Check if the device type already exists in the assembly
                    bool deviceExistsInAssembly = await DeviceAlreadyExists(deviceAddRequest.OperationId, deviceAddRequest.DeviceTypeId, deviceAddRequest.AssemblyId);
                    if (deviceExistsInAssembly)
                    {
                        throw new InvalidOperationException("You can't assign the same device type to the same assembly line");
                    }

                    var existingDevice = MockDb.DbDevices.FirstOrDefault(d => d.DeviceId == operation.DeviceId);
                    if (existingDevice != null)
                    {
                        // Remove the existing device if it's different from the new device
                        if (existingDevice.DeviceType != (DeviceType)deviceAddRequest.DeviceTypeId)
                        {
                            MockDb.DbDevices.Remove(existingDevice);
                        }
                        else
                        {
                            // Replace the existing device's name
                            existingDevice.Name = deviceAddRequest.Name;
                            return await Task.FromResult(new DeviceResponse { DeviceId = existingDevice.DeviceId, Name = existingDevice.Name });
                        }
                    }

                    // Add the new device since it's a new type for the assembly
                    var toDeviceEntity = deviceAddRequest.ToDeviceEntity();
                    MockDb.DbDevices.Add(toDeviceEntity);

                    bool assignmentSuccess = await AssignDeviceToAssemblyOperation(deviceAddRequest.OperationId, toDeviceEntity.DeviceId, deviceAddRequest.AssemblyId);
                    if (!assignmentSuccess)
                    {
                        throw new InvalidOperationException("Was unable to assign a device to the operation");
                    }

                    return await Task.FromResult(new DeviceResponse { DeviceId = toDeviceEntity.DeviceId, Name = toDeviceEntity.Name });
                }
                else
                {
                    throw new InvalidOperationException("The specified operation does not exist");
                }
            }

            // Logic for cases where OperationId is 0 or less
            var newDeviceEntity = deviceAddRequest.ToDeviceEntity();
            MockDb.DbDevices.Add(newDeviceEntity);
            return await Task.FromResult(new DeviceResponse { DeviceId = newDeviceEntity.DeviceId, Name = newDeviceEntity.Name });
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

            // Update the operation with the new device ID, regardless of whether it already has a device
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

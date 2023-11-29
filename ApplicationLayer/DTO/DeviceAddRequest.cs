using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLine.ApplicationLayer.DTO
{
    public class DeviceAddRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DeviceType DeviceType { get; set; }
        public int AssemblyId { get; set; }
        public int OperationId { get; set; }

        public Device ToDeviceEntity()
        {
            return new Device
            {
                DeviceId = Id,
                Name = Name,
                DeviceType = DeviceType,

            };
        }
    }

   
}

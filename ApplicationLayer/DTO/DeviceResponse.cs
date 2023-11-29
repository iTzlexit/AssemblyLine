using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLine.ApplicationLayer.DTO
{
    public class DeviceResponse
    {
        public int DeviceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DeviceType DeviceType { get; set; }
    }

}

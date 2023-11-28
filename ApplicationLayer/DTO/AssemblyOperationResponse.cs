using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    /// <summary>
    /// retrieve operations based on assemblies 
    /// </summary>
    public class AssemblyOperationResponseForDevice
    {
        public List<AssemblyResponse> Assemblies { get; set; } = new List<AssemblyResponse>();
        public List<string> DeviceTypes { get; set; } = Enum.GetNames(typeof(DeviceType)).ToList();

    }
}

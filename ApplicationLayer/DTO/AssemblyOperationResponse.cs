using AssemblyLine.ApplicationLayer.DTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLine.ApplicationLayer.DTO
{
    /// <summary>
    /// retrieve operations based on assemblies 
    /// </summary>
    public class ResponseForDeviceTypes
    {

        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

    }

  

}

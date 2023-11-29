using AssemblyLine.ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLine.ApplicationLayer.DTO
{
    public class AssemblyResponse
    {
        public int Id { get; set; }
        public string AssemblyName { get; set; } = string.Empty;
        public List<OperationResponse> Operations { get; set; } = new List<OperationResponse>();
    }
}

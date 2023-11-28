using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class OperationResponse
    {
        public int OperationId { get; set; }
        public string Name { get; set; } = string.Empty;

        public int AssemblyId { get; set; }
        public int OrderInWhichToPerform { get; set; }
        public byte[] ImageData { get; set; } = new byte[0]; //store image of item being manufatured 

        public string DeviceName { get; set; } = string.Empty; 

    }

    public static class OperationExtensions
    {
        public static IEnumerable<OperationResponse> ToOperationResponse(this IEnumerable<Operation> operations)
        {
            return operations.Select(o => new OperationResponse
            {
                OperationId = o.OperationId,
                Name = o.OperationName,
                OrderInWhichToPerform = o.OrderInWhichToPerform,
                ImageData = o.ImageData,
                DeviceName = o.Device.Name 
            }).ToList();

        }


    }
}

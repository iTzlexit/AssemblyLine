using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLine.ApplicationLayer.DTO
{
    public class OperationAddRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1, 4)]
        public int OrderInWhichToPerform { get; set; }
        public byte[] ImageData { get; set; } = new byte[0]; //store image of item being manufatured 

        // Foreign key
        public int DeviceId { get; set; }

        public int AssemblyId { get; set; }


        public Operation ToOperationEntity()
        {
            return new Operation
            {
                OperationId = Id,
                OperationName = Name.Trim(),
                OrderInWhichToPerform = OrderInWhichToPerform,
                ImageData = ImageData,
                AssemblyId = AssemblyId,
                DeviceId = DeviceId


            };
        }
    }
}

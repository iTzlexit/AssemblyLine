using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DeviceType DeviceType { get; set; }


    }



    public enum DeviceType
    {
        BarcodeScanner =1,
        Printer = 2,
        Camera = 3,
        SocketTray = 4, 
    }
}

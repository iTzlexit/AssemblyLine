using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.InMemory
{
    public static class MockDb
    {


        public static List<Device> Devices { get; private set; }
        public static List<Operation> Operations { get; private set; }

        static MockDb()
        {
            InitializeDevices();
            InitializeOperations();
        }

        private static void InitializeDevices()
        {
            Devices = new List<Device>
            {
                new Device { DeviceId = 1, Name = "Scanner 1", DeviceType = DeviceType.BarcodeScanner },
                new Device { DeviceId = 2, Name = "Printer A", DeviceType = DeviceType.Printer },
                new Device { DeviceId = 3, Name = "Camera X", DeviceType = DeviceType.Camera },
                new Device { DeviceId = 4, Name = "Socket Tray Z", DeviceType = DeviceType.SocketTray }
            };
        }

        private static void InitializeOperations()
        {
            Operations = new List<Operation>
            {
                new Operation { OperationId = 1, Name = "Scan Items", OrderInWhichToPerform = 1, ImageData = new byte[0], DeviceId = 1 },
                new Operation { OperationId = 2, Name = "Print Labels", OrderInWhichToPerform = 2, ImageData = new byte[0], DeviceId = 2 },
                new Operation { OperationId = 3, Name = "Capture Images", OrderInWhichToPerform = 3, ImageData = new byte[0], DeviceId = 3 },
                new Operation { OperationId = 4, Name = "Organize Trays", OrderInWhichToPerform = 4, ImageData = new byte[0], DeviceId = 4 }
            };
        }


        //Non static way 

        //public List<Device> Devices { get; private set; }
        //public List<Operation> Operations { get; private set; }

        //public MockDb()
        //{
        //    InitializeDevices();
        //    InitializeOperations();
        //}

        //private void InitializeDevices()
        //{
        //    Devices = new List<Device>
        //    {
        //        new Device { DeviceId = 1, Name = "Scanner 1", DeviceType = DeviceType.BarcodeScanner },
        //        new Device { DeviceId = 2, Name = "Printer A", DeviceType = DeviceType.Printer },
        //        new Device { DeviceId = 3, Name = "Camera X", DeviceType = DeviceType.Camera },
        //        new Device { DeviceId = 4, Name = "Socket Tray Z", DeviceType = DeviceType.SocketTray }

        //    };


        //}

        //private void InitializeOperations()
        //{
        //    Operations = new List<Operation>
        //    {
        //        new Operation { OperationId = 1, Name = "Scan Items", OrderInWhichToPerform = 1, ImageData = new byte[0], DeviceId = 1 },
        //        new Operation { OperationId = 2, Name = "Print Labels", OrderInWhichToPerform = 2, ImageData = new byte[0], DeviceId = 2 },
        //        new Operation { OperationId = 3, Name = "Capture Images", OrderInWhichToPerform = 3, ImageData = new byte[0], DeviceId = 3},
        //        new Operation { OperationId = 4, Name = "Organize Trays", OrderInWhichToPerform = 4, ImageData = new byte[0], DeviceId = 4 }
        //    };

        //}
    }
}

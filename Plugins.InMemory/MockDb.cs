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
        public static List<Assemblies> DbAssemblies { get; set; }
        public static List<Operation> DbOperations { get; private set; }
        public static List<Device> DbDevices { get; private set; }


        static MockDb()
        {
            InitializeAssemblies();
            InitializeDevices();
            InitializeOperations();
        }

        private static void InitializeAssemblies()
        {

            DbAssemblies = new List<Assemblies>
            {
                  new Assemblies { Id = 1, AssemblyName = "Assembly Line 1" },
                  new  Assemblies { Id = 2, AssemblyName = "Assembly Line 2" },
                  new Assemblies { Id = 3, AssemblyName = "Assembly Line 3" }
            };
        }

        private static void InitializeDevices()
        {
            DbDevices = new List<Device>
            {
                new Device { DeviceId = 1, Name = "Scanner 1", DeviceType = DeviceType.BarcodeScanner },
                new Device { DeviceId = 2, Name = "Printer A", DeviceType = DeviceType.Printer },
                new Device { DeviceId = 3, Name = "Camera X", DeviceType = DeviceType.Camera },
                new Device { DeviceId = 4, Name = "Socket Tray Z", DeviceType = DeviceType.SocketTray }
            };
        }

        private static void InitializeOperations()
        {
            DbOperations = new List<Operation>
            {
                new Operation { OperationId = 1, OperationName = "Scan Items", OrderInWhichToPerform = 1, ImageData = new byte[0], DeviceId = 1, AssemblyId = 1 },
                new Operation { OperationId = 2, OperationName = "Print Labels", OrderInWhichToPerform = 2, ImageData = new byte[0], DeviceId = 2, AssemblyId = 1 },
                new Operation { OperationId = 3, OperationName = "Capture Images", OrderInWhichToPerform = 3, ImageData = new byte[0], DeviceId = 3, AssemblyId = 1 },
                new Operation { OperationId = 4, OperationName = "Organize Trays", OrderInWhichToPerform = 4, ImageData = new byte[0], DeviceId = 4, AssemblyId = 1 }
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

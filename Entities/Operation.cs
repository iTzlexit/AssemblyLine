namespace Entities
{
    public class Operation
    {
        public int OperationId { get; set; }
        public string OperationName { get; set; } = string.Empty;
        public int OrderInWhichToPerform { get; set; }
        public byte[] ImageData { get; set; } = new byte[0]; //store image of item being manufatured 

        // foreignKey 
        public int AssemblyId { get; set; }

        // Foreign key
        //One to one relationship  where each operation is associated with one specific device. 
        public int DeviceId { get; set; }
        // Navigation property
        public Device Device { get; set; } = new Device(); // completes the operation 
    }
}

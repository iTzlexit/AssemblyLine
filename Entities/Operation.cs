namespace Entities
{
    public class Operation
    {
        public int OperationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OrderInWhichToPerform { get; set; }
        public byte[] ImageData { get; set; } = new byte[0]; //store image of item being manufatured 

        // Foreign key
        public int DeviceId { get; set; }
        // Navigation property
        public Device Device { get; set; } = new Device(); // completes the operation 
    }
}

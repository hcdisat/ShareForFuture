namespace ShareForFuture.DataAccess
{
    public class DeviceImage: ModelBase
    {
        public int DeviceId { get; set; }

        public string Path { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;        
    }
}

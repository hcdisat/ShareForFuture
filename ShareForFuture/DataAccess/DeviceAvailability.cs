namespace ShareForFuture.DataAccess
{
    public class DeviceAvailability: ModelBase
    {
        public int DeviceId { get; set; }

        public int StatusId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Device Device { get; set; } = new();

        public DeviceStatus Status { get; set; } = new();
    }
}

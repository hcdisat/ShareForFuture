namespace ShareForFuture.DataAccess
{
    public class DeviceStatus: ModelBase
    {
        public string Name { get; set; } = string.Empty;

        public List<DeviceAvailability> Availabilities { get; set; } = new();
    }
}

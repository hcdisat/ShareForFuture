namespace ShareForFuture.DataAccess
{
    public class DeviceTag: ModelBase
    {
        public string Name { get; set; } = string.Empty;

        public List<Device> Devices { get; set; } = new();
    }
}

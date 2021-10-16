namespace ShareForFuture.DataAccess
{
    public class DeviceCategory : ModelBase
    {
        public string Name { get; set; } = string.Empty;

        public int ParentCategoryId { get; set; }

        public DeviceCategory ParentCategory { get; set; } = new();

        public List<DeviceCategory> DeviceCategories { get; set; } = new();

        public List<Device> Devices { get; set; } = new();
    }
}

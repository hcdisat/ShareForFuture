namespace ShareForFuture.DataAccess
{
    public class DeviceVerification: ModelBase
    {
        public int DeviceId { get; set; }

        public bool IsDataValid { get; set; } = true;

        public Device Device { get; set; } = new();
    }
}

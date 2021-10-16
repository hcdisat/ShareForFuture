namespace ShareForFuture.DataAccess
{
    public class OfferingNotificationType: ModelBase
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public List<OfferingNotification> Notifications { get; set; } = new();

    }
}

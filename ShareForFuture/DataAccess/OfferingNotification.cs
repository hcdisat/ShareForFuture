namespace ShareForFuture.DataAccess
{
    public class OfferingNotification: ModelBase
    {
        public int OfferingId { get; set; }

        public int TypeId { get; set; }

        public OfferingNotificationType Type { get; set; } = new();

        public Offering Offering { get; set; } = new();
    }
}

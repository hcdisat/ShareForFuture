namespace ShareForFuture.DataAccess
{
    public class Offering: ModelBase
    {
        public int? BorrowerId { get; set; }

        public int LenderId { get; set; }

        public int DeviceId { get; set; }

        public DateTimeOffset? From { get; set; }

        public DateTimeOffset? To { get; set; }

        public User Borrower { get; set; } = new();

        public User Lender { get; set; } = new();

        public Device Device { get; set; } = new();

        public List<OfferingStatus> Statuses { get; set; } = new();

        public List<Review> Reviews { get; set; } = new();
    }
}

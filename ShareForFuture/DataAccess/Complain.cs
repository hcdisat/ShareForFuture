namespace ShareForFuture.DataAccess
{
    public class Complain: ModelBase
    {
        public int OfferingId { get; set; }

        public int FillerId { get; set; }

        public int AgainsToId { get; set; }

        public int ComplainStatusId { get; set; }

        public string Body { get; set; } = string.Empty;

        public DateTimeOffset? ClosedDate { get; set; }

        public User Filler { get; set; } = new();

        public User AgainstTo { get; set; } = new();

        public Offering Offering { get; set; } = new();

        public ComplainStatus ComplainStatus { get; set; } = new();

        public List<ComplainNote> Notes { get; set; } = new();

        public List<ComplainAsignment> Asignments { get; set; } = new();

        public List<ComplainImage> Images { get; set; } = new();
    }

    public class ComplainStatus : ModelBase
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<Complain> Complains { get; set; } = new();
    }
}

namespace ShareForFuture.DataAccess
{
    public class ComplainAsignment : ModelBase
    {
        public int ComplainId { get; set; }

        public int AssignedById { get; set; }

        public int AssignedToId { get; set; }

        public Complain Complain { get; set; } = new();
    }

    public class ComplainNote : ModelBase
    {
        public int ComplainId { get; set; }

        public string Body { get; set; } = string.Empty;

        public Complain Complain { get; set; } = new();
    }

    public class ComplainImage : ModelBase
    {
        public int ComplainId { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public Complain Complain { get; set; } = new();
    }
}

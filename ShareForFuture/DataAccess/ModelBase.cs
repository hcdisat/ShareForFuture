namespace ShareForFuture.DataAccess
{
    public class ModelBase
    {
        public int Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}

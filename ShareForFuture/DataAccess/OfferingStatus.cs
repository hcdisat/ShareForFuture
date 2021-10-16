namespace ShareForFuture.DataAccess
{
    public class OfferingStatus: ModelBase
    {
        public string Name { get; set; } = string.Empty;

        public string Description {  get; set; } = string.Empty;

        public List<Offering> Offerings { get; set; } = new();
    }
}

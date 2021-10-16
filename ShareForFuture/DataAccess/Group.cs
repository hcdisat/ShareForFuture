namespace ShareForFuture.DataAccess
{
    public class Group: ModelBase
    {
        public string Name { get; set; } = string.Empty;

        public List<User> Users { get; set; } = new();
    }
}

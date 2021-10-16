namespace ShareForFuture.DataAccess
{
    public class IdentityProvider: ModelBase
    {
        public int UserId { get; set; }

        public string Identifier { get; set; } = string.Empty;

        public string ProviderName {  get; set; } = string.Empty;

        public User User { get; set; } = new();
    }
}

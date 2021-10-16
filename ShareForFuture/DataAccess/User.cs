namespace ShareForFuture.DataAccess
{
    public class User: ModelBase
    {
        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;

        public EmailVerification EmailVerification { get; set; } = new();

        public List<IdentityProvider> IdentityProviders { get; set; } = new();

        public List<LoginHistory> LoginHistories { get; set; } = new();

        public List<Group> Groups { get; set; } = new();

        public List<Device> Devices { get; set; } = new();
    }
}

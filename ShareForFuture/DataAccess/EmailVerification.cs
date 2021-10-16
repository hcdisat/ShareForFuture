using System.Text.Json.Serialization;

namespace ShareForFuture.DataAccess
{
    public class EmailVerification: ModelBase
    {
        public int UserId { get; set; }

        public string Token { get; set; } = string.Empty;
        
        public User User { get; set; } = new();
    }
}

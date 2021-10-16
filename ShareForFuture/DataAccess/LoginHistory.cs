namespace ShareForFuture.DataAccess
{
    public class LoginHistory: ModelBase
    {
        public int UserId { get; set; }

        public User User { get; set; } = new();
    }
}

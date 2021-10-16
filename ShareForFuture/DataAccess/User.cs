using System.ComponentModel.DataAnnotations;

namespace ShareForFuture.DataAccess
{
    public class User : ModelBase
    {
        [MaxLength(50), Required]
        public string Firstname { get; set; } = string.Empty;

        [MaxLength(50), Required]
        public string Lastname { get; set; } = string.Empty;
        
        
        [MaxLength(50), Required]        
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Address { get; set; } = string.Empty;

        public EmailVerification EmailVerification { get; set; } = new();

        public List<IdentityProvider> IdentityProviders { get; set; } = new();

        public List<LoginHistory> LoginHistories { get; set; } = new();

        public List<Group> Groups { get; set; } = new();

        public List<Device> Devices { get; set; } = new();

        public List<Offering> BorrowerOfferings { get; set; } = new();

        public List<Offering> LenderOfferings { get; set; } = new();

        public List<Review> BorrowerReviews { get; set; } = new();

        public List<Review> LenderReviews { get; set; } = new();

        public List<Complain> FillerComplains { get; internal set; } = new();

        public List<Complain> AgainstToComplains { get; internal set; } = new();
    }
}

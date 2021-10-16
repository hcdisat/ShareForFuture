using System.ComponentModel;

namespace ShareForFuture.DataAccess
{
    public class Device: ModelBase
    {
        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public User User { get; set; } = new();

        public Condition Condition { get; set; }

        public DeviceCategory Category { get; set; } = new();

        public DeviceVerification DeviceVerification { get; set; } = new();

        public List<DeviceAvailability> Availabilities { get; set; } = new();
        
        public List<DeviceImage> Images { get; set; } = new();

        public List<DeviceTag> Tags { get; set; } = new();
    }

    public enum Condition
    {
        New,
        Used,

        [Description("Heavily Used")]
        HeavilyUser,

        [Description("To Be Repaired")]
        ToBeRepaired
    }
}

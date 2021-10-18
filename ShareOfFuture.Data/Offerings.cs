using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShareForFuture.Data;

// Requirement: Every user can offer 0..many devices for sharing. 
public class Offering : ModelBase
{
    // Requirement: S4F must store basic description data including title, description, condition
    // (like new, used, heavily used, to be repaired) and 0..many images of the device.
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public Condition Condition { get; set; } = Condition.Used;

    // Requirement: S4F wants to be able to send out regular (e.g. every month)
    // reminder emails to people offering devices asking whether the data about the device is still valid.
    public DateTimeOffset LastVerificationDate { get; set; }

    public int? OwnerId { get; set; }
    public User? Owner { get; set; }

    public List<OfferingImage > Images { get; set; } = new();

    // Requirement: Each device must be assigned to a subcategory.
    public int? CategoryId { get; set; }
    public OfferingCategory? Category { get; set; }

    public List<OfferingTag> Tags { get; set; } = new();

    // Requirement: Users can mark devices that they offer as currently available or currently unavailable.
    // Requirement: S4F wants to be able to send reminder emails to users who have marked a device as currently unavailable for a long time
    public DateTimeOffset? MarkedAsUnavailableDate { get; set; }


    // Requirement: Users can store periods of time through which a device will not be
    // available(e.g. if the lender needs the device on their own for a planned home improvement project).
    public OfferingUnavailabilityPeriod? UnavailabilityPeriod { get; set; }

    public List<Sharing> Sharings { get; set; } = new();
}

class OfferingIdentityConfiguration : IEntityTypeConfiguration<Offering>
{
    public void Configure(EntityTypeBuilder<Offering> builder)
    {
        builder.Property(d => d.Title).HasMaxLength(200);

        builder.Property(d => d.LastVerificationDate)
            .HasDefaultValue(DateTimeOffset.Now);
        
        builder.HasOne(d => d.Owner)
            .WithMany(u => u.Offerings)
            .HasForeignKey(d => d.OwnerId)
            .IsRequired(false);

        builder.HasMany(d => d.Tags)
            .WithMany(t => t.Offerings);

        builder.HasOne(d => d.UnavailabilityPeriod)
            .WithOne(up => up.Offering)
            .HasForeignKey<OfferingUnavailabilityPeriod>(up => up.OfferingId)
            .IsRequired(false);
    }
}

// Requirement: S4F contains a list of device categories. 
public class OfferingCategory: ModelBase
{
    public string Name { get; set; } = string.Empty;

    // Requirement: Each category consists of a list of subcategories.
    public int? ParentCategoryId { get; set; }
    public OfferingCategory? ParentCategory { get; set; }
    
    public List<OfferingCategory> ChildrenCategories { get; set; } = new();

    public List<Offering> Offerings { get; set; } = new();
}

public class OfferingCategoryConfiguration : IEntityTypeConfiguration<OfferingCategory>
{
    public void Configure(EntityTypeBuilder<OfferingCategory> builder)
    {
        builder.HasIndex(c => c.Name).IsUnique();
        builder.Property(c => c.Name).HasMaxLength(100);

        builder.HasOne(c => c.ParentCategory)
            .WithMany(c => c.ChildrenCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasMany(c => c.Offerings)
           .WithOne(d => d.Category)
           .HasForeignKey(d => d.CategoryId)
           .OnDelete(DeleteBehavior.Restrict)
           .IsRequired(false); 
    }
}

// Requirement: S4F must store 0..many images of the device.
public class OfferingImage : ModelBase
{
    public string ImageURL { get; set; } = string.Empty;

    public int? OfferingId { get; set; }
    public Offering? Offering { get; set; }
}

public enum Condition
{
    New,
    Used,
    HeavilyUser,
    ToBeRepaired
}

// Requirement: Users offering devices can assign 0..many tags to each device.
public class OfferingTag: ModelBase
{
    // Requirement: A tag is just a text property.
    public string Tag { get; set; } = string.Empty;

    public List<Offering> Offerings { get; set; } = new();
}

// Requirement: Users can mark devices that they offer as currently available or currently unavailable.
public class OfferingUnavailabilityPeriod: ModelBase
{
    public DateTimeOffset From { get; set; }

    public DateTimeOffset? Until { get; set; }

    public int? OfferingId { get; set; }

    public Offering? Offering { get; set; }
}

public class OfferingUnavailabilityPeriodEntityConfiguration : IEntityTypeConfiguration<OfferingUnavailabilityPeriod>
{
    public void Configure(EntityTypeBuilder<OfferingUnavailabilityPeriod> builder)
    {
        builder.HasCheckConstraint("UntilAfterFrom", 
            @$"[{nameof(OfferingUnavailabilityPeriod.Until)}] IS NULL
                OR [{nameof(OfferingUnavailabilityPeriod.Until)}] > [{nameof(OfferingUnavailabilityPeriod.From)}]");
    }
}
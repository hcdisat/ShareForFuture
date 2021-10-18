using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShareForFuture.Data;

// Requirement: S4F must store a list of users.
public class User : ModelBase
{
    // Requirement: S4F must store a contact email address.
    public string Email { get; set; } = string.Empty;

    // Requirement: S4F must store a list of users.
    public string Firstname { get; set; } = string.Empty;
    
    public string Lastname { get; set; } = string.Empty;
    
    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;
    
    public string Country { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public int? GroupId { get; set; }
    public UserGroup? Group { get; set; }

    // Requirement: The platform will contain mechanisms to validate contact email addresses
    public DateTimeOffset? EmailVerifiedDate { get; set; }

    // Requirement: S4F wants to be able to send reminder emails to users who have not logged in to S4F for a long time.
    // The S4F database must be able to store the necessary data to enable that feature.
    public DateTimeOffset? LastLogingDate { get; set; }

    // Requirement:
    // S4F does not store user names and passwords.The system relies on external identity providers
    // (Google, Microsoft, and Facebook). Every user can associate her account with 1..many identities
    // (e.g.one user could sign in with her Google or her Facebook account).
    // For each identity, S4F needs to store the identity provider and the technical user ID of the corresponding identity provider.
    public IdentityProvider IdentityProvider { get; set; }

    // Requirement: Every user can offer 0..many devices for sharing.
    public List<Offering> Offerings { get; set; } = new();

    public List<Sharing> Borrows { get; set; } = new();

    public List<Complain> Complains { get; set; } = new();

    public List<Complain> ComplainsAbout { get; set; } = new();

    public List<Complain> AssignedComplains { get; set; } = new();
}

class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).UseIdentityColumn();
        builder.Property(u => u.Firstname).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Lastname).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Street).IsRequired().HasMaxLength(150);
        builder.Property(u => u.City).IsRequired().HasMaxLength(50);
        builder.Property(u => u.State).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Country).IsRequired().HasMaxLength(50);
        builder.Property(u => u.ZipCode).IsRequired().HasMaxLength(6);
        builder.Property(u => u.PhoneNumber).IsRequired(false).HasMaxLength(11);        
        builder.Property(u => u.EmailVerifiedDate).IsRequired(false);

        // Requirement: The email address must be globally unique 
        builder.Property(u => u.Email).IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasOne(u => u.Group)
            .WithMany(ug => ug.Users)
            .HasForeignKey(u => u.GroupId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}

/// <summary>
/// Requirements:
/// Users who are S4F employees are marked in the database. 
/// </summary>
public class UserGroup : ModelBase
{
    public string Name { get; set; } = string.Empty;

    public List<User> Users { get; set; } = new();
}

// Requirement: Users who are S4F employees are marked in the database. 
public class UserGroupIdentityConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired().HasMaxLength(50);

        builder.HasIndex(g => g.Name).IsUnique();

        // They can belong to one of the following user groups:
        // ## Regular S4F employee
        // ## Manager
        // ## System administrator
        builder.HasData(new UserGroup[] 
        {
            new UserGroup{ Id = 1, Name = "S4FEmployee" },
            new UserGroup{ Id = 2, Name = "Manager" },
            new UserGroup{ Id = 3, Name = "SystemAdministrator" },
        });
    }
}

public enum IdentityProvider
{
    Google,
    Microsoft,
    Facebook
}

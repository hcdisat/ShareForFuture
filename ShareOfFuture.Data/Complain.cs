using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShareForFuture.Data;

public class Complain: ModelBase
{
    // Requirement: Users can file complaints about other users
    // (e.g. borrower did return the borrowed device broken).
    public int? ComplainerId { get; set; }
    public User? Complainer { get; set; }

    public int? ComplaineeId { get; set; }
    public User? Complainee { get; set; }

    // Requirement: S4F employees will try to settle the complaints.
    // When a complaint comes in, a S4F manager assigns it to an S4F employee who will be responsible for the complaint.  
    public int? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }

    public List<ComplainNote> Notes { get; set; } = new();
}

public class ComplainEntityConfiguration : IEntityTypeConfiguration<Complain>
{
    public void Configure(EntityTypeBuilder<Complain> builder)
    {
        builder.HasOne(c => c.Complainer)
            .WithMany(u => u.Complains)
            .HasForeignKey(c => c.ComplainerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Complainee)
            .WithMany(u => u.ComplainsAbout)
            .HasForeignKey(c => c.ComplaineeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.AssignedTo)
            .WithMany(u => u.AssignedComplains)
            .HasForeignKey(c => c.AssignedToId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Notes)
            .WithOne(n => n.Complain)
            .HasForeignKey(n => n.ComplainId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

// Requirement: Both involved users and the assigned S4F employee can store notes (text, pictures) to the complaint.
// The S4F employee can mark the complaint as done once it will have been settled.
public class ComplainNote : ModelBase
{
    public int? ComplainId { get; set; }

    public Complain? Complain { get; set; }

    public string? Note { get; set; }

    public byte[]? Picture { get; set; }

    public DateTimeOffset? DoneDate { get; set; }
}

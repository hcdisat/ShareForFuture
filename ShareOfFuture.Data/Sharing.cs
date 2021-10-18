
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShareForFuture.Data;

public class Sharing: ModelBase
{
    // Requirement: Users find a device they want to borrow,
    public int? BorrowerId { get; set; }   
    public User? Borrower { get; set; }

    // they can enter the period of time through which they would like to have the tool.
    public int? OfferingId { get; set; }
    public Offering? Offering { get; set; }

    public DateTimeOffset From { get; set; }

    public DateTimeOffset Until { get; set; }

    //Requirement: S4F will send a notification email about the borrow request to the lender of the device.
    //S4F wants to be able to send reminder emails to lenders if they do not respond within 48 hours.
    public DateTimeOffset? LastRequestNotificationSentDate { get; set; }

    // Requirement: The lender can accept or decline the borrow request.
    public bool? OfferingWasAccepted { get; set; }
    public DateTimeOffset? AcceptedDeclinedDate { get; set; }

    // Requirement: the lender can add a message (e.g. greeting, reason why declined). The borrower receives a corresponding notification email.
    public string AcceptOrDeclineMessage { get; set; } = string.Empty;

    // Requirement: The lender can mark the share as active once the borrower picked the device up.
    // S4F needs to record the time when the share becomes active.
    public DateTimeOffset? ShareActivationDate { get; set; }

    // Requirement: When the borrower returns the device, the lender marks the share as done.
    public DateTimeOffset? ShareDoneDate { get; set; }

    // Requirement:
    // The lender can rank the borrower with 0..5 stars.
    // The borrower can rank the lender with 0..5 stars.
    // The borrower can also rank the device with 0..5 stars.
    // Both users can add a textual note to rankings (e.g. reason why to many stars).
    public int? LenderToBorrowerRating { get; set; }    
    public int? BorrowerToLenderRating { get; set; }
    public int? BorrowerToOffering { get; set; }

    public string LenderToBorrowerNotes { get; set; } = string.Empty;
    public string BorrowerToLenderNotes { get; set; } = string.Empty;
    public string BorrowerToOfferingNotes { get; set; } = string.Empty;
}

public class SharingIdentityConfiguration : IEntityTypeConfiguration<Sharing>
{
    public void Configure(EntityTypeBuilder<Sharing> builder)
    {
        builder.HasOne(s => s.Borrower)
            .WithMany(u => u.Borrows)
            .HasForeignKey(s => s.BorrowerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasOne(S => S.Offering)
            .WithMany(o => o.Sharings)
            .HasForeignKey(o => o.OfferingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasCheckConstraint("UntilAfterFrom", 
            $@"[{nameof(Sharing.Until)}] > [{nameof(Sharing.From)}]");
    }
}



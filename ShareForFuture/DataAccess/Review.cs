using System.ComponentModel;

namespace ShareForFuture.DataAccess
{
    public class Review: ModelBase
    {
        public int BorrowerId { get; set; }

        public int LenderId { get; set; }

        public int OfferingId { get; set; }

        public ReviewType Type { get; set; }

        public int? Score { get; set; }

        public int? DiviceScore { get; set; }

        public string Body { get; set; } = string.Empty;

        public User Borrower { get; set; } = new();

        public User Lender { get; set; } = new();

        public Offering Offering { get; set; } = new();
    }

    public enum ReviewType
    {
        [Description("Lender Type")]
        Lender,

        [Description("Borrower Type")]
        Borrower
    }
}

using System;

namespace Blueshift.Sample.Entities;

/// <summary>
/// Represents a loan for a given <see cref="Book"/> to a <see cref="Member"/>.
/// </summary>
public class BookLoan
{
    /// <summary>
    /// The unique identifier for the current <see cref="BookLoan"/>.
    /// </summary>
    public Guid? BookLoanId { get; set; }

    /// <summary>
    /// The <see cref="Member"/> to whom <see cref="Lent"/> has been loaned.
    /// </summary>
    public Member? Borrower { get; set; }

    /// <summary>
    /// The <see cref="Book"/> that was lent.
    /// </summary>
    public Book? Lent { get; set; }

    /// <summary>
    /// The time at which the current <see cref="BookLoan"/> was made.
    /// </summary>
    public DateTime LoanTime { get; set; }

    /// <summary>
    /// The date on which the current <see cref="BookLoan"/> is due.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// The time at which <see cref="Lent"/> was returned.
    /// </summary>
    public DateTime? ReturnedTime { get; set; }

    /// <summary>
    /// The time at which the current <see cref="BookLoan" /> was last modified.
    /// </summary>
    public DateTime? LastModifiedTime { get; set; }

    /// <summary>
    /// Contains data from the underlying persistence store regarding the version of the current <see cref="BookLoan" />.
    /// </summary>
    public byte[]? Version { get; set; }
}

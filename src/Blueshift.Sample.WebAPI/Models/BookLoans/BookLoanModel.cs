using System;
using Blueshift.Sample.Adapters.Repositories.SqlServer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blueshift.Sample.WebAPI.Models.BookLoans;

/// <summary>
/// Represents a Member within the Sample applicaition.
/// </summary>
public class BookLoanModel
{
    /// <summary>
    /// The unique identifier for the current <see cref="BookLoanModel"/>.
    /// </summary>
    [Key]
    public Guid? BookLoanId { get; set; }

    /// <summary>
    /// The <see cref="SqlMember"/> to whom <see cref="Lent"/> has been loaned.
    /// </summary>
    public SqlMember? Borrower { get; set; }

    /// <summary>
    /// The <see cref="SqlBook"/> that was lent.
    /// </summary>
    public SqlBook? Lent { get; set; }

    /// <summary>
    /// The time at which the current <see cref="BookLoanModel"/> was made.
    /// </summary>
    public DateTime LoanTime { get; set; }

    /// <summary>
    /// The date on which the current <see cref="BookLoanModel"/> is due.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// The time at which <see cref="Lent"/> was returned.
    /// </summary>
    public DateTime? ReturnedTime { get; set; }

    /// <summary>
    /// The date the current <see cref="BookLoanModel"/> was most recently modified.
    /// </summary>
    public DateTime LastModifiedTime { get; set; }

    /// <summary>
    /// Contains information about the version of the current <see cref="BookLoanModel" />.
    /// </summary>
    [Timestamp]
    public byte[]? Version { get; set; }
}

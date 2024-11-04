using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blueshift.Sample.Adapters.Repositories.SqlServer.Entities;

/// <summary>
/// Represents a loan for a <see cref="SqlBook"/> that has been checked out to a given <see cref="SqlMember"/>.
/// </summary>
public class SqlBookLoan
{
    /// <summary>
    /// The unique identifier for the current <see cref="SqlBookLoan"/>.
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
    /// The time at which the current <see cref="SqlBookLoan"/> was made.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime LoanTime { get; set; }

    /// <summary>
    /// The date on which the current <see cref="SqlBookLoan"/> is due.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// The time at which <see cref="Lent"/> was returned.
    /// </summary>
    public DateTime? ReturnedTime { get; set; }

    /// <summary>
    /// The date the current <see cref="SqlBookLoan"/> was most recently modified.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime LastModifiedTime { get; set; }

    /// <summary>
    /// Contains information about the version of the current <see cref="SqlBookLoan" />.
    /// </summary>
    [Timestamp]
    public byte[]? Version { get; set; }
}

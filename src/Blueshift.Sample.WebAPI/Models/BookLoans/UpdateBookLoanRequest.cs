using System;

namespace Blueshift.Sample.WebAPI.Models.BookLoans;

/// <summary>
/// A request to update an existing Member record.
/// </summary>
public class UpdateBookLoanRequest : CreateBookLoanRequest
{
    /// <summary>
    /// The unique identifier of the <see cref="BookLoanModel"/>.
    /// </summary>
    public Guid BookLoanId { get; set; }

    /// <summary>
    /// Contains data from the underlying persistence store regarding the version of the current <see cref="BookLoanModel" />.
    /// </summary>
    public byte[]? Version { get; set; }
}

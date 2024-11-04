using System;

namespace Blueshift.Sample.WebAPI.Models.BookLoans;

/// <summary>
/// A request to create a new Member.
/// </summary>
public class CreateBookLoanRequest
{
    /// <summary>
    /// The title of the current <see cref="BookLoanModel"/>.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The subtitle of the current <see cref="BookLoanModel"/>.
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// The date the current <see cref="BookLoanModel"/> was published.
    /// </summary>
    public DateTime? PublishDate { get; set; }
}

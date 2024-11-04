using System;

namespace Blueshift.Sample.WebAPI.Models.Books;

/// <summary>
/// A request to create a new Member.
/// </summary>
public class CreateBookRequest
{
    /// <summary>
    /// The title of the current <see cref="BookModel"/>.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The subtitle of the current <see cref="BookModel"/>.
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// The date the current <see cref="BookModel"/> was published.
    /// </summary>
    public DateTime? PublishDate { get; set; }
}

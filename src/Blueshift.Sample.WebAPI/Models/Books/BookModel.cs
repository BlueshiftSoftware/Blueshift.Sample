using System;

namespace Blueshift.Sample.WebAPI.Models.Books;

/// <summary>
/// Represents a Member within the Sample applicaition.
/// </summary>
public class BookModel
{
    /// <summary>
    /// The unique identifier of the current <see cref="BookModel"/>.
    /// </summary>
    public Guid? BookId { get; set; }

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

    /// <summary>
    /// The date the current <see cref="BookModel"/> was created.
    /// </summary>
    public DateTime? CreatedTime { get; set; }

    /// <summary>
    /// The date the current <see cref="BookModel"/> was most recently modified.
    /// </summary>
    public DateTime? LastModifiedTime { get; set; }

    /// <summary>
    /// Contains data from the underlying persistence store regarding the version of the current <see cref="BookModel" />.
    /// </summary>
    public byte[]? Version { get; set; }
}

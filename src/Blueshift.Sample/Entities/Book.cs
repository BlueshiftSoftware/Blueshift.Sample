using System;

namespace Blueshift.Sample.Entities;

/// <summary>
/// A core entity representing a book.
/// </summary>
public class Book
{
    /// <summary>
    /// The unique identifier of the current <see cref="Book"/>.
    /// </summary>
    public Guid? BookId { get; set; }

    /// <summary>
    /// The title of the current <see cref="Book"/>.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The subtitle of the current <see cref="Book"/>.
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// The date the current <see cref="Book"/> was created.
    /// </summary>
    public DateTime? CreatedTime { get; set; }

    /// <summary>
    /// The date the current <see cref="Book"/> was published.
    /// </summary>
    public DateTime? PublishDate { get; set; }

    /// <summary>
    /// The date the current <see cref="Book"/> was most recently modified.
    /// </summary>
    public DateTime? LastModifiedTime { get; set; }

    /// <summary>
    /// Contains data from the underlying persistence store regarding the version of the current <see cref="Book" />.
    /// </summary>
    public byte[]? Version { get; set; }
}

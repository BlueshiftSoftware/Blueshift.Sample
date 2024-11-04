using System;

namespace Blueshift.Sample.WebAPI.Models.Books;

/// <summary>
/// A request to update an existing Member record.
/// </summary>
public class UpdateBookRequest : CreateBookRequest
{
    /// <summary>
    /// The unique identifier of the <see cref="BookModel"/>.
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// Contains data from the underlying persistence store regarding the version of the current <see cref="BookModel" />.
    /// </summary>
    public byte[]? Version { get; set; }
}

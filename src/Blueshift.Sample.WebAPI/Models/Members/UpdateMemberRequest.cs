using System;
using System.Text;
using Blueshift.Sample.WebAPI.Models.Books;

namespace Blueshift.Sample.WebAPI.Models.Members;

/// <summary>
/// A request to update an existing Member record.
/// </summary>
public class UpdateMemberRequest : CreateMemberRequest
{
    /// <summary>
    /// The unique identifier of the Member.
    /// </summary>
    public Guid MemberId { get; set; }

    /// <summary>
    /// Contains data from the underlying persistence store regarding the version of the current <see cref="BookModel" />.
    /// </summary>
    public byte[]? Version { get; set; }
}

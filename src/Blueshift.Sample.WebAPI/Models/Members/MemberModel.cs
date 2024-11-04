using System;

namespace Blueshift.Sample.WebAPI.Models.Members;

/// <summary>
/// Represents a Member within the Sample applicaition.
/// </summary>
public class MemberModel
{
    /// <summary>
    /// The unique id of the Member.
    /// </summary>
    public Guid? MemberId { get; set; }

    /// <summary>
    /// The Member's given name.
    /// </summary>
    public string? GivenName { get; set; }

    /// <summary>
    /// The Member's surname.
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    /// A timestamp representing the time this <see cref="MemberModel"/> was initially created.
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// A timestamp representing the most recent time this <see cref="MemberModel"/> was modified.
    /// </summary>
    public DateTime LastModifiedTime { get; set; }

    /// <summary>
    /// Contains data from the underlying persistence store regarding the version of the current <see cref="MemberModel" />.
    /// </summary>
    public byte[]? Version { get; set; }
}

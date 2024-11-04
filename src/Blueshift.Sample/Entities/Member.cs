using System;

namespace Blueshift.Sample.Entities;

/// <summary>
/// Represents a Member of a library.
/// </summary>
public class Member
{
    /// <summary>
    /// The Member's unique identifier.
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
    /// A timestamp representing the time this <see cref="Member"/> was initially created.
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// A timestamp representing the most recent time this <see cref="Member"/> was modified.
    /// </summary>
    public DateTime LastModifiedTime { get; set; }

    /// <summary>
    /// Contains data from the underlying persistence store regarding the version of the current <see cref="Member" />.
    /// </summary>
    public byte[]? Version { get; set; }
}

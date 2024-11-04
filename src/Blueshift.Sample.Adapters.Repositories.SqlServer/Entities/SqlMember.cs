using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blueshift.Sample.Entities;

namespace Blueshift.Sample.Adapters.Repositories.SqlServer.Entities;

/// <summary>
/// A database record that represents a <see cref="Member"/> core entity.
/// </summary>
public class SqlMember
{
    /// <summary>
    /// The unique identifier for the current <see cref="SqlMember"/>.
    /// </summary>
    [Key]
    public Guid? MemberId { get; set; }

    /// <summary>
    /// The given name of the current <see cref="SqlMember"/>.
    /// </summary>
    public string? GivenName { get; set; }

    /// <summary>
    /// The surname of the current <see cref="SqlMember"/>.
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    /// The date the current <see cref="SqlMember"/> was initially created.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// The date the current <see cref="SqlMember"/> was most recently modified.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime LastModifiedTime { get; set; }

    /// <summary>
    /// Contains information about the version of the current <see cref="SqlMember" />.
    /// </summary>
    [Timestamp]
    public byte[]? Version { get; set; }
}

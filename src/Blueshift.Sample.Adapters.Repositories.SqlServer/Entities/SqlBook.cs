using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blueshift.Sample.Entities;

namespace Blueshift.Sample.Adapters.Repositories.SqlServer.Entities;

/// <summary>
/// A database record that represents a <see cref="Book"/> core entity.
/// </summary>
public class SqlBook
{
    /// <summary>
    /// The unique identifier of the current <see cref="SqlBook"/>.
    /// </summary>
    [Key]
    public Guid? BookId { get; set; }

    /// <summary>
    /// The title of the current <see cref="SqlBook"/>.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The subtitle of the current <see cref="SqlBook"/>.
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// The date the current <see cref="SqlBook"/> was published.
    /// </summary>
    public DateTime? PublishDate { get; set; }

    /// <summary>
    /// The date the current <see cref="SqlBook"/> was initially created.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// The date the current <see cref="SqlBook"/> was most recently modified.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime LastModifiedTime { get; set; }

    /// <summary>
    /// Contains information about the version of the current <see cref="SqlBook" />.
    /// </summary>
    [Timestamp]
    public byte[]? Version { get; set; }
}

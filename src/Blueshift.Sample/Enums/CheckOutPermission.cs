using Blueshift.Sample.Entities;

namespace Blueshift.Sample.Enums;

/// <summary>
/// An enumerated list of library book check out permission statuses.
/// </summary>
public enum CheckOutPermission
{
    /// <summary>
    /// The <see cref="Member"/> is allowed to check out more books.
    /// </summary>
    Allowed,

    /// <summary>
    /// The <see cref="Member"/> is not allowed to check out any more books because they have at least one overdue loan.
    /// </summary>
    HasOverdue,

    /// <summary>
    /// The <see cref="Member"/> is not allowed to check out any more books because they have reached their outstanding loan limit.
    /// </summary>
    MaximumReached,
}

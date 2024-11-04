using System;
using System.Threading.Tasks;
using Blueshift.Sample.Entities;
using Blueshift.Sample.Enums;

namespace Blueshift.Sample.Services;

/// <summary>
/// A service for working with <see cref="BookLoan"/> entities.
/// </summary>
public interface IBookLoanService
{
    /// <summary>
    /// Returns a value indicating whether a given <see cref="Member"/> is allowed to check out more books.
    /// </summary>
    /// <param name="memberId">The unique identifier of the <see cref="Member"/> to check.</param>
    /// <returns><see cref="CheckOutPermission"/> indicating the <see cref="Member">Member's</see> current permission status.</returns>
    Task<CheckOutPermission> GetCheckOutPermissionStatus(Guid memberId);
}

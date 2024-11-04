using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blueshift.Sample.Entities;

namespace Blueshift.Sample.Ports.Repositories;

/// <summary>
/// A repository for CRUD operations against <see cref="Member"/> entities.
/// </summary>
public interface IMemberRepository
{
    /// <summary>
    /// Saves a new <see cref="Member"/> record to the underlying persistence store.
    /// </summary>
    /// <param name="member">The new <see cref="Member"/> to be saved.</param>
    /// <returns>A copy of <paramref name="member" />, with any relevant changes made while persisting.</returns>
    Task<Member> CreateMemberAsync(Member member);

    /// <summary>
    /// Gets a single <see cref="Member"/> record from the underlying persistence store.
    /// </summary>
    /// <param name="memberId">The unique identifier of the <see cref="Member"/> to retrieve.</param>
    /// <returns>A <see cref="Member"/> instance, if one could be found; otherwise <c>null</c>.</returns>
    Task<Member?> GetMemberByIdAsync(Guid memberId);

    /// <summary>
    /// Get all <see cref="Member"/> entity records.
    /// </summary>
    /// <returns>An <see cref="IReadOnlyCollection{T}"/> of <see cref="Member"/> containing all members in the current repository.</returns>
    Task<IReadOnlyCollection<Member>> GetMembersAsync();

    /// <summary>
    /// Updates a <see cref="Member"/> record in the underlying persistence store.
    /// </summary>
    /// <param name="member">An existing <see cref="Member"/> record to update.</param>
    /// <returns>A copy of <paramref name="member" />, with any relevant changes made while persisting.</returns>
    Task<Member?> UpdateMemberAsync(Member member);

    /// <summary>
    /// Deletes the <see cref="Member"/> record of the specified <paramref name="memberId"/>.
    /// </summary>
    /// <param name="memberId">The unique identifier of the <see cref="Member"/> to delete.</param>
    /// <returns>A <see cref="Task"/> containing information related to the results of the operation.</returns>
    Task DeleteMemberAsync(Guid memberId);
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blueshift.Sample.Entities;

namespace Blueshift.Sample.Ports.Repositories;

/// <summary>
/// A repository for storing data for <see cref="BookLoan"/> records.
/// </summary>
public interface IBookLoanRepository
{
    /// <summary>
    /// Saves a new <see cref="BookLoan"/> record to the underlying persistence store.
    /// </summary>
    /// <param name="bookLoan">The new <see cref="BookLoan"/> to be saved.</param>
    /// <returns>A copy of <paramref name="bookLoan" />, with any relevant changes made while persisting.</returns>
    Task<BookLoan> CreateBookLoanAsync(BookLoan bookLoan);

    /// <summary>
    /// Gets a single <see cref="BookLoan"/> record from the underlying persistence store.
    /// </summary>
    /// <param name="bookLoanId">The unique identifier of the <see cref="BookLoan"/> to retrieve.</param>
    /// <returns>A <see cref="BookLoan"/> instance, if one could be found; otherwise <c>null</c>.</returns>
    Task<BookLoan?> GetBookLoanByIdAsync(Guid bookLoanId);

    /// <summary>
    /// Gets all <see cref="BookLoan"/> records for a given <paramref name="memberId"/>.
    /// </summary>
    /// <param name="memberId">The unique identifier of the <see cref="Member"/> for whom to retrieve <see cref="BookLoan"/> records.</param>
    /// <param name="outstandingOnly">An optional flag indicating whether to retrieve only recofds for outstanding (ie: un-returned) loans.</param>
    /// <returns>An <see cref="IReadOnlyCollection{T}"/> of <see cref="BookLoan"/> for <paramref name="memberId"/>.</returns>
    Task<IReadOnlyCollection<BookLoan>> GetBookLoansByMemberIdAsync(Guid memberId, bool outstandingOnly = false);

    /// <summary>
    /// Gets all <see cref="BookLoan"/> records for a given <paramref name="bookId"/>.
    /// </summary>
    /// <param name="bookId">The unique identifier of the <see cref="Member"/> for which to retrieve <see cref="BookLoan"/> records.</param>
    /// <param name="outstandingOnly">An optional flag indicating whether to retrieve only recofds for outstanding (ie: un-returned) loans.</param>
    /// <returns>An <see cref="IReadOnlyCollection{T}"/> of <see cref="BookLoan"/> for <paramref name="bookId"/>.</returns>
    Task<IReadOnlyCollection<BookLoan>> GetBookLoansByBookIdAsync(Guid bookId, bool outstandingOnly = false);

    /// <summary>
    /// Gets all <see cref="BookLoan"/> entity records.
    /// </summary>
    /// <returns>An <see cref="IReadOnlyCollection{T}"/> of <see cref="BookLoan"/> containing all BookLoans in the current repository.</returns>
    Task<IReadOnlyCollection<BookLoan>> GetBookLoansAsync();

    /// <summary>
    /// Updates a <see cref="BookLoan"/> record in the underlying persistence store.
    /// </summary>
    /// <param name="bookLoan">An existing <see cref="BookLoan"/> record to update.</param>
    /// <returns>A copy of <paramref name="bookLoan" />, with any relevant changes made while persisting.</returns>
    Task<BookLoan?> UpdateBookLoanAsync(BookLoan bookLoan);
}

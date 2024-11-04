using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blueshift.Sample.Entities;

namespace Blueshift.Sample.Ports.Repositories;

/// <summary>
/// Represents a repository for storing information about <see cref="Book"/> entities.
/// </summary>
public interface IBookRepository
{
    /// <summary>
    /// Saves a new <see cref="Book"/> record to the underlying persistence store.
    /// </summary>
    /// <param name="book">The new <see cref="Book"/> to be saved.</param>
    /// <returns>A copy of <paramref name="book" />, with any relevant changes made while persisting.</returns>
    Task<Book> CreateBookAsync(Book book);

    /// <summary>
    /// Gets a single <see cref="Book"/> record from the underlying persistence store.
    /// </summary>
    /// <param name="bookId">The unique identifier of the <see cref="Book"/> to retrieve.</param>
    /// <returns>A <see cref="Book"/> instance, if one could be found; otherwise <c>null</c>.</returns>
    Task<Book?> GetBookByIdAsync(Guid bookId);

    /// <summary>
    /// Get all <see cref="Book"/> entity records.
    /// </summary>
    /// <returns>An <see cref="IReadOnlyCollection{T}"/> of <see cref="Book"/> containing all Books in the current repository.</returns>
    Task<IReadOnlyCollection<Book>> GetBooksAsync();

    /// <summary>
    /// Updates a <see cref="Book"/> record in the underlying persistence store.
    /// </summary>
    /// <param name="book">An existing <see cref="Book"/> record to update.</param>
    /// <returns>A copy of <paramref name="book" />, with any relevant changes made while persisting.</returns>
    Task<Book?> UpdateBookAsync(Book book);

    /// <summary>
    /// Deletes the <see cref="Book"/> record of the specified <paramref name="bookId"/>.
    /// </summary>
    /// <param name="bookId">The unique identifier of the <see cref="Book"/> to delete.</param>
    /// <returns>A <see cref="Task"/> containing information related to the results of the operation.</returns>
    Task DeleteBookAsync(Guid bookId);
}

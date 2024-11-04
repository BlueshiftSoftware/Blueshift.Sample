using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blueshift.Sample.Adapters.Repositories.SqlServer.Entities;
using Blueshift.Sample.Entities;
using Blueshift.Sample.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blueshift.Sample.Adapters.Repositories.SqlServer;

/// <inheritdoc />
/// <param name="sampleContext">An instance of a <see cref="SampleContext"/> for working with <see cref="SqlBook"/> records.</param>
/// <param name="mapper">An instance of <see cref="IMapper"/> for mapping between core entities and their database records.</param>
public class BookRepository(SampleContext sampleContext, IMapper mapper) : IBookRepository
{
    private readonly SampleContext _sampleContext = sampleContext ?? throw new ArgumentNullException(nameof(sampleContext));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    /// <inheritdoc />
    public async Task<Book> CreateBookAsync(Book book)
    {
        var sqlBook = mapper.Map<SqlBook>(book);

        try
        {
            await _sampleContext.Books.AddAsync(sqlBook);
            await _sampleContext.SaveChangesAsync();
        }
        catch
        {
            // TODO: check exception here
            throw;
        }

        return _mapper.Map<Book>(sqlBook);
    }

    /// <inheritdoc />
    public async Task<Book?> GetBookByIdAsync(Guid bookId)
    {
        Book? book = null;

        try
        {
            SqlBook? sqlBook = await _sampleContext.Books.FindAsync(bookId);
            if (sqlBook is not null)
            {
                book = _mapper.Map<Book>(sqlBook);
            }
        }
        catch
        {
            // TODO: check exception here
            throw;
        }

        return book;
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<Book>> GetBooksAsync()
    {
        IReadOnlyCollection<Book> books = _mapper.ProjectTo<Book>(_sampleContext.Books)
            .ToList()
            .AsReadOnly();

        return Task.FromResult(books);
    }

    /// <inheritdoc />
    public async Task<Book?> UpdateBookAsync(Book book)
    {
        var sqlBook = mapper.Map<SqlBook>(book);

        try
        {
            _sampleContext.Books.Update(sqlBook);
            await _sampleContext.SaveChangesAsync();
        }
        catch
        {
            // TODO: check exception here
            throw;
        }

        return _mapper.Map<Book>(sqlBook);
    }

    /// <inheritdoc />
    public async Task DeleteBookAsync(Guid bookId)
    {
        try
        {
            int count = await _sampleContext.Books
                .Where(book => book.BookId == bookId)
                .ExecuteDeleteAsync();

            // TODO: check deleted count
        }
        catch
        {
            // TODO: check exception here
            throw;
        }
    }
}

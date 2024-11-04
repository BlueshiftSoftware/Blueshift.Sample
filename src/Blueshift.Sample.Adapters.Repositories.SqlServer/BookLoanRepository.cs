using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using Blueshift.Sample.Adapters.Repositories.SqlServer.Entities;
using Blueshift.Sample.Entities;
using Blueshift.Sample.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blueshift.Sample.Adapters.Repositories.SqlServer;

/// <inheritdoc />
/// <param name="sampleContext">An instance of a <see cref="SampleContext"/> for working with <see cref="SqlBookLoan"/> records.</param>
/// <param name="mapper">An instance of <see cref="IMapper"/> for mapping between core entities and their database records.</param>
public class BookLoanRepository(SampleContext sampleContext, IMapper mapper) : IBookLoanRepository
{
    private readonly SampleContext _sampleContext = sampleContext ?? throw new ArgumentNullException(nameof(sampleContext));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    /// <inheritdoc />
    public async Task<BookLoan> CreateBookLoanAsync(BookLoan bookLoan)
    {
        var sqlBookLoan = mapper.Map<SqlBookLoan>(bookLoan);

        try
        {
            await _sampleContext.BookLoans.AddAsync(sqlBookLoan);
            await _sampleContext.SaveChangesAsync();
        }
        catch
        {
            // TODO: check exception here
            throw;
        }

        return _mapper.Map<BookLoan>(sqlBookLoan);
    }

    /// <inheritdoc />
    public async Task<BookLoan?> GetBookLoanByIdAsync(Guid bookLoanId)
    {
        BookLoan? bookLoan = null;

        try
        {
            SqlBookLoan? sqlBookLoan = await _sampleContext.BookLoans.FindAsync(bookLoanId);
            if (sqlBookLoan is not null)
            {
                bookLoan = _mapper.Map<BookLoan>(sqlBookLoan);
            }
        }
        catch
        {
            // TODO: check exception here
            throw;
        }

        return bookLoan;
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<BookLoan>> GetBookLoansAsync()
    {
        IReadOnlyCollection<BookLoan> bookLoans = _mapper.ProjectTo<BookLoan>(_sampleContext.BookLoans)
            .ToList()
            .AsReadOnly();

        return Task.FromResult(bookLoans);
    }

    /// <inheritdoc />
    public async Task<BookLoan?> UpdateBookLoanAsync(BookLoan bookLoan)
    {
        var sqlBookLoan = mapper.Map<SqlBookLoan>(bookLoan);

        try
        {
            _sampleContext.BookLoans.Update(sqlBookLoan);
            await _sampleContext.SaveChangesAsync();
        }
        catch
        {
            // TODO: check exception here
            throw;
        }

        return _mapper.Map<BookLoan>(sqlBookLoan);
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<BookLoan>> GetBookLoansByMemberIdAsync(Guid memberId, bool outstandingOnly = false)
    {
        IQueryable<SqlBookLoan> query = _sampleContext.BookLoans
            .Where(bookLoan => bookLoan.Borrower!.MemberId == memberId);

        if (outstandingOnly)
        {
            query = query.Where(bookLoan => bookLoan.ReturnedTime == null);
        }

        IReadOnlyCollection<BookLoan> bookLoans = _mapper.ProjectTo<BookLoan>(query)
            .ToList()
            .AsReadOnly();

        return Task.FromResult(bookLoans);
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<BookLoan>> GetBookLoansByBookIdAsync(Guid bookId, bool outstandingOnly = false)
    {
        IQueryable<SqlBookLoan> query = _sampleContext.BookLoans
            .Where(bookLoan => bookLoan.Lent!.BookId == bookId);

        if (outstandingOnly)
        {
            query = query.Where(bookLoan => bookLoan.ReturnedTime == null);
        }

        IReadOnlyCollection<BookLoan> bookLoans = _mapper.ProjectTo<BookLoan>(query)
            .ToList()
            .AsReadOnly();

        return Task.FromResult(bookLoans);
    }
}

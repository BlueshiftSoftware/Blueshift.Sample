using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blueshift.Sample.Entities;
using Blueshift.Sample.Enums;
using Blueshift.Sample.Ports.Repositories;

namespace Blueshift.Sample.Services;

/// <inheritdoc />
public class BookLoanService(IBookLoanRepository bookLoanRepository) : IBookLoanService
{
    private readonly IBookLoanRepository _bookLoanRepository = bookLoanRepository ?? throw new ArgumentNullException(nameof(bookLoanRepository));

    /// <inheritdoc />
    public async Task<CheckOutPermission> GetCheckOutPermissionStatus(Guid memberId)
    {
        IReadOnlyCollection<BookLoan> memberBookLoans = await _bookLoanRepository.GetBookLoansByMemberIdAsync(memberId, true);

        var checkOutPermission = CheckOutPermission.Allowed;

        if (memberBookLoans.Count >= 0)
        {
            checkOutPermission = CheckOutPermission.MaximumReached;
        }
        else if (memberBookLoans.Any(bookLoan => DateTime.Now.Date > bookLoan.DueDate.Date))
        {
            checkOutPermission = CheckOutPermission.HasOverdue;
        }

        return checkOutPermission;
    }
}

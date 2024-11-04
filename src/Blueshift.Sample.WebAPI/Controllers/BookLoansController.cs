using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blueshift.Sample.Entities;
using Blueshift.Sample.Ports.Repositories;
using Blueshift.Sample.WebAPI.Models;
using Blueshift.Sample.WebAPI.Models.BookLoans;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blueshift.Sample.WebAPI.Controllers;

/// <summary>
/// WebAPI controller for BookLoan models.
/// </summary>
/// <param name="mapper">An instance of <see cref="IMapper"/> for mapping between business entities and their models.</param>
/// <param name="logger">An instance of <see cref="ILogger{T}"/> for logging the controller's activity.</param>
/// <param name="bookLoanRepository">An instance of <see cref="IBookLoanRepository"/> to use for retrieving and updating <see cref="BookLoan"/> data.</param>
[ApiController]
[Route("[controller]")]
public class BookLoansController(
    ILogger<BookLoansController> logger,
    IMapper mapper,
    IBookLoanRepository bookLoanRepository) : ControllerBase
{
    private readonly ILogger<BookLoansController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IBookLoanRepository _bookLoanRepository = bookLoanRepository ?? throw new ArgumentNullException(nameof(bookLoanRepository));

    /// <summary>
    /// Gets a list of available BookLoans.
    /// </summary>
    /// <returns>A collection of <see cref="BookLoanModel"/> instances.</returns>
    /// <response code="200">Returns a collection of <see cref="BookLoanModel"/> instances.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PageModel<BookLoanModel>>> GetAsync(
        CancellationToken requestCancellationToken,
        [FromQuery] int? page = null,
        [FromQuery] int? pageSize = null)
    {
        using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(requestCancellationToken);
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        IEnumerable<BookLoan> bookLoans = await _bookLoanRepository.GetBookLoansAsync();
        IEnumerable<BookLoanModel> bookLoanModels = _mapper.Map<IEnumerable<BookLoanModel>>(bookLoans);

        var pageModel = new PageModel<BookLoanModel>(bookLoanModels)
        {
            CurrentPage = page ?? -1,
            PageSize = pageSize ?? -1,
            TotalItems = 100
        };

        return Ok(pageModel);
    }

    /// <summary>
    /// Gets the model for a specific BookLoan record.
    /// </summary>
    /// <param name="bookLoanId">The unique identifier of the BookLoan to fetch.</param>
    /// <returns>A single <see cref="BookLoanModel"/>, if found.</returns>
    /// <response code="200">Returns a single <see cref="BookLoanModel"/>, if found.</response>
    /// <response code="404">Returns a <see cref="NotFoundResult"/> if the requested application cannot be found.</response>
    [HttpGet("{bookLoanId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookLoanModel>> Get(Guid bookLoanId)
    {
        BookLoan? bookLoan = await _bookLoanRepository.GetBookLoanByIdAsync(bookLoanId);
        return bookLoan != null
            ? Ok(_mapper.Map<BookLoanModel>(bookLoan))
            : NotFound();
    }

    /// <summary>
    /// Creates a new BookLoan record.
    /// </summary>
    /// <param name="createBookLoanRequest">The request model containing the values for the new BookLoan.</param>
    /// <returns>The newly created <see cref="BookLoanModel"/>, if successful.</returns>
    /// <response code="201">Returns the newly created <see cref="BookLoanModel"/>, if successful.</response>
    /// <response code="400">Returns a <see cref="BadRequestResult"/> if the request fails.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookLoanModel>> Post([FromBody] CreateBookLoanRequest createBookLoanRequest)
    {
        BookLoan newBookLoan = _mapper.Map<BookLoan>(createBookLoanRequest);
        newBookLoan = await _bookLoanRepository.CreateBookLoanAsync(newBookLoan);
        BookLoanModel bookLoanModel = _mapper.Map<BookLoanModel>(newBookLoan);
        return Created($"/bookLoans/{newBookLoan.BookLoanId}", bookLoanModel);
    }


    /// <summary>
    /// Updates a BookLoan record.
    /// </summary>
    /// <param name="bookLoanId">The unique identifier for the BookLoan to update.</param>
    /// <param name="updateBookLoanRequest">The request model containing the new settings for the BookLoan.</param>
    /// <returns>The updated <see cref="BookLoanModel"/>, if successful.</returns>
    /// <response code="200">Returns updated <see cref="BookLoanModel"/>, if successful.</response>
    /// <response code="400">Returns a <see cref="BadRequestResult"/> if the request fails.</response>
    /// <response code="404">Returns a <see cref="NotFoundResult"/> if application to be udpated cannot be found.</response>
    [HttpPut("{bookLoanId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookLoanModel>> Put(
        Guid bookLoanId,
        [FromBody] UpdateBookLoanRequest updateBookLoanRequest)
    {
        if (bookLoanId != updateBookLoanRequest.BookLoanId)
        {
            throw new Exception("The bookLoanId in the request route must match the id in the POST body.");
        }

        BookLoan? newBookLoan = _mapper.Map<BookLoan>(updateBookLoanRequest);
        newBookLoan = await _bookLoanRepository.UpdateBookLoanAsync(newBookLoan);
        BookLoanModel bookLoanModel = _mapper.Map<BookLoanModel>(newBookLoan);
        return Ok(bookLoanModel);
    }
}

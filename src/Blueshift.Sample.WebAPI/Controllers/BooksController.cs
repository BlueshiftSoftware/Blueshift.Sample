using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blueshift.Sample.Entities;
using Blueshift.Sample.Ports.Repositories;
using Blueshift.Sample.WebAPI.Models;
using Blueshift.Sample.WebAPI.Models.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blueshift.Sample.WebAPI.Controllers;

/// <summary>
/// WebAPI controller for Book models.
/// </summary>
/// <param name="mapper">An instance of <see cref="IMapper"/> for mapping between business entities and their models.</param>
/// <param name="logger">An instance of <see cref="ILogger{T}"/> for logging the controller's activity.</param>
/// <param name="bookRepository">An instance of <see cref="IBookRepository"/> to use for retrieving and updating <see cref="Book"/> data.</param>
[ApiController]
[Route("[controller]")]
public class BooksController(
    ILogger<BooksController> logger,
    IMapper mapper,
    IBookRepository bookRepository) : ControllerBase
{
    private readonly ILogger<BooksController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IBookRepository _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));

    /// <summary>
    /// Gets a list of available Books.
    /// </summary>
    /// <returns>A collection of <see cref="BookModel"/> instances.</returns>
    /// <response code="200">Returns a collection of <see cref="BookModel"/> instances.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PageModel<BookModel>>> GetAsync(
        CancellationToken requestCancellationToken,
        [FromQuery] int? page = null,
        [FromQuery] int? pageSize = null)
    {
        using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(requestCancellationToken);
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        IEnumerable<Book> books = await _bookRepository.GetBooksAsync();
        IEnumerable<BookModel> bookModels = _mapper.Map<IEnumerable<BookModel>>(books);

        var pageModel = new PageModel<BookModel>(bookModels)
        {
            CurrentPage = page ?? -1,
            PageSize = pageSize ?? -1,
            TotalItems = 100
        };

        return Ok(pageModel);
    }

    /// <summary>
    /// Gets the model for a specific Book record.
    /// </summary>
    /// <param name="bookId">The unique identifier of the Book to fetch.</param>
    /// <returns>A single <see cref="BookModel"/>, if found.</returns>
    /// <response code="200">Returns a single <see cref="BookModel"/>, if found.</response>
    /// <response code="404">Returns a <see cref="NotFoundResult"/> if the requested application cannot be found.</response>
    [HttpGet("{bookId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookModel>> Get(Guid bookId)
    {
        Book? book = await _bookRepository.GetBookByIdAsync(bookId);
        return book != null
            ? Ok(_mapper.Map<BookModel>(book))
            : NotFound();
    }

    /// <summary>
    /// Creates a new Book record.
    /// </summary>
    /// <param name="createBookRequest">The request model containing the values for the new Book.</param>
    /// <returns>The newly created <see cref="BookModel"/>, if successful.</returns>
    /// <response code="201">Returns the newly created <see cref="BookModel"/>, if successful.</response>
    /// <response code="400">Returns a <see cref="BadRequestResult"/> if the request fails.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookModel>> Post([FromBody] CreateBookRequest createBookRequest)
    {
        Book newBook = _mapper.Map<Book>(createBookRequest);
        newBook = await _bookRepository.CreateBookAsync(newBook);
        BookModel bookModel = _mapper.Map<BookModel>(newBook);
        return Created($"/books/{newBook.BookId}", bookModel);
    }


    /// <summary>
    /// Updates a Book record.
    /// </summary>
    /// <param name="bookId">The unique identifier for the Book to update.</param>
    /// <param name="updateBookRequest">The request model containing the new settings for the Book.</param>
    /// <returns>The updated <see cref="BookModel"/>, if successful.</returns>
    /// <response code="200">Returns updated <see cref="BookModel"/>, if successful.</response>
    /// <response code="400">Returns a <see cref="BadRequestResult"/> if the request fails.</response>
    /// <response code="404">Returns a <see cref="NotFoundResult"/> if application to be udpated cannot be found.</response>
    [HttpPut("{bookId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookModel>> Put(
        Guid bookId,
        [FromBody] UpdateBookRequest updateBookRequest)
    {
        if (bookId != updateBookRequest.BookId)
        {
            throw new Exception("The bookId in the request route must match the id in the POST body.");
        }

        Book? newBook = _mapper.Map<Book>(updateBookRequest);
        newBook = await _bookRepository.UpdateBookAsync(newBook);
        BookModel bookModel = _mapper.Map<BookModel>(newBook);
        return Ok(bookModel);
    }

    /// <summary>
    /// Deletes a Book.
    /// </summary>
    /// <param name="bookId">The unique identifier for the Book to delete.</param>
    /// <returns>A single <see cref="BookModel"/>, if found.</returns>
    /// <response code="204">Returns a <see cref="NoContentResult"/> if successful.</response>
    /// <response code="404">Returns a <see cref="NotFoundResult"/> if the requested Book cannot be found.</response>
    [HttpDelete("{bookId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid bookId)
    {
        await _bookRepository.DeleteBookAsync(bookId);
        return NoContent();
    }
}

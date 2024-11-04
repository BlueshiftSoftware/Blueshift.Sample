using System;
using System.Collections.Generic;

namespace Blueshift.Sample.WebAPI.Models;

/// <summary>
/// Represents a page of results from a search request.
/// </summary>
/// <remarks>
/// Initializes a new intance of the <see cref="PageModel{T}"/> class.
/// </remarks>
/// <param name="items">The collection of <typeparamref name="T"/> items in the current page.</param>
public class PageModel<T>(IEnumerable<T> items)
{
    /// <summary>
    /// The total number of <typeparamref name="T"/> items available in the current search.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// The current page of results.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The requested page size.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// A collection of the <typeparamref name="T"/> items in the current page.
    /// </summary>
    public ICollection<T> Items { get; } = new List<T>(items ?? throw new ArgumentNullException(nameof(items)));
}

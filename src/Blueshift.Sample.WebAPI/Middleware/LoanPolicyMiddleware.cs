using System.Threading.Tasks;
using Blueshift.Sample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Blueshift.Sample.WebAPI.Middleware;

/// <summary>
/// A middleware service for enforcing a per-member book loan limit policy.
/// </summary>
/// <param name="next">The next <see cref="RequestDelegate"/> to execute, if any.</param>
public class LoanPolicyMiddleware(RequestDelegate? next)
{
    private readonly RequestDelegate? _next = next;

    /// <summary>
    /// Asynchronously invokes this <see cref="LoanPolicyMiddleware"/>.
    /// </summary>
    /// <param name="context">An <see cref="HttpContext"/> containing information about the current request.</param>
    /// <param name="bookLoanService">An instance of <see cref="IBookLoanService"/> to use to fetch information about outstanding book loans.</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context, IBookLoanService bookLoanService)
    {
        RouteData routeData = context.GetRouteData();

        //routeData.Values: action, controller, [route param]...

        if (_next != null)
        {
            await _next(context);
        }
    }
}

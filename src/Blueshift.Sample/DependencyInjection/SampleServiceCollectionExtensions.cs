using Blueshift.Sample.Services;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Blueshift.Sample;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// Adds the Sample core dependencies to an <see cref="IServiceCollection"/> instance.
/// </summary>
public static class SampleServiceCollectionExtensions
{
    /// <summary>
    /// Adds the core Sample depedencies to an instance of <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the dependencies will be added.</param>
    /// <returns><paramref name="services"/>, so that calls may be chained.</returns>
    public static IServiceCollection AddSampleCore(this IServiceCollection services)
    {
        return services
            .AddScoped<IBookLoanService, BookLoanService>();
    }
}

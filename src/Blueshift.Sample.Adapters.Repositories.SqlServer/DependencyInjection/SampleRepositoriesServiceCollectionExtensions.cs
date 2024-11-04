using Blueshift.Sample.Adapters.Repositories.SqlServer;
using Blueshift.Sample.Adapters.Repositories.SqlServer.MappingProfiles;
using Blueshift.Sample.Ports.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Blueshift.Sample;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// Adds the SqlServer adapter dependencies to an <see cref="IServiceCollection"/> instance.
/// </summary>
public static class SampleRepositoriesServiceCollectionExtensions
{
    /// <summary>
    /// Adds the SqlServer repository adapters to an instance of <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the dependencies will be added.</param>
    /// <param name="connectionString">A string used to connect to the SqlServer instance.</param>
    /// <returns><paramref name="services"/>, so that calls may be chained.</returns>
    public static IServiceCollection AddSqlServerRepositories(this IServiceCollection services, string connectionString)
    {
        return services
            .AddAutoMapper(typeof(SampleDbProfile))
            .AddDbContext<SampleContext>(builder =>
                builder.UseSqlServer(connectionString)
            )
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IBookLoanRepository, BookLoanRepository>()
            .AddScoped<IMemberRepository, MemberRepository>();
    }
}

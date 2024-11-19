using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blueshift.Sample.Adapters.Repositories.SqlServer.Migrations;

/// <summary>
/// A factory for creating instances of <see cref="SampleContext"/> for use with <code>dotnet ef migrations</code>.
/// </summary>
public class MigrationContextFactory : IDesignTimeDbContextFactory<SampleContext>
{
    /// <summary>
    /// Creates a new instance of <see cref="SampleContext"/> for use with migrations.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public SampleContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SampleContext>();
        string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__SampleSqlServerDb")
            ?? "Server=.\\;Database=Sample;Trusted_Connection=True;";
        optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
        return new SampleContext(optionsBuilder.Options);
    }
}

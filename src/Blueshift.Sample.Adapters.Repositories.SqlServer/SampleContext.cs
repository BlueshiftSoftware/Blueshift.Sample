using Blueshift.Sample.Adapters.Repositories.SqlServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blueshift.Sample.Adapters.Repositories.SqlServer;

/// <summary>
/// A <see cref="DbContext"/> for the Sample application.
/// </summary>
/// <param name="options">A set of <see cref="DbContextOptions{TContext}"/> to use when operating against the Sample database.</param>
public class SampleContext(DbContextOptions<SampleContext> options) : DbContext(options)
{
    /// <summary>
    /// A <see cref="DbSet{T}"/> for working with <see cref="SqlMember"/> records.
    /// </summary>
    public DbSet<SqlMember> Members { get; private set; }

    /// <summary>
    /// A <see cref="DbSet{T}"/> for working with <see cref="SqlBook"/> records.
    /// </summary>
    public DbSet<SqlBook> Books { get; private set; }

    /// <summary>
    /// A <see cref="DbSet{T}"/> for working with <see cref="SqlBookLoan"/> records.
    /// </summary>
    public DbSet<SqlBookLoan> BookLoans { get; private set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SqlBook>()
            .Property(book => book.CreatedTime)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<SqlBook>()
            .Property(book => book.LastModifiedTime)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<SqlBookLoan>()
            .Property(sqlBookLoan => sqlBookLoan.LoanTime)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<SqlBookLoan>()
            .Property(member => member.LastModifiedTime)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<SqlMember>()
            .Property(member => member.CreatedTime)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<SqlMember>()
            .Property(member => member.LastModifiedTime)
            .HasDefaultValueSql("getdate()");
    }
}

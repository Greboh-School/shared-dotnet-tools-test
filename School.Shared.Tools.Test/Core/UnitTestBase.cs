using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace School.Shared.Tools.Test.Core;

[Collection("Sequential")] // Add all UnitTests to same collection in order to run sequentially
public abstract class UnitTestBase<TContext> : IDisposable where TContext : DbContext
{
    protected TContext Context;
    private SqliteConnection _connection;

    protected UnitTestBase()
    {
        // Using Guid to ensure unique
        var inMemoryConnectionString = $"DataSource=DatabaseName-{Guid.NewGuid()};mode=memory;cache=shared";
        _connection = new(inMemoryConnectionString);

        var ctxOptions = new DbContextOptionsBuilder<TContext>()
            .UseSqlite(_connection)
            .Options;
        
        _connection.Open();

        Context = Activator.CreateInstance(typeof(TContext), ctxOptions) as TContext
                  ?? throw new ArgumentOutOfRangeException(typeof(TContext).ToString(), $"{typeof(TContext)} could not be resolved");
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Context.Dispose();
        _connection.Close();
        _connection.Dispose();
    }
}
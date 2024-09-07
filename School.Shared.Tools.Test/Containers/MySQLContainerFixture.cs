using Testcontainers.MySql;

namespace School.Shared.Tools.Test.Containers;

public class MySQLContainerFixture : IDisposable
{
    public static string ConnectionString = default!;

    private readonly MySqlContainer _container;

    public MySQLContainerFixture()
    {
        _container = new MySqlBuilder().Build();
        _container.StartAsync().Wait();

        ConnectionString = _container.GetConnectionString();

        // It can cause issues if the connection string is not 127.0.0.1
        if (ConnectionString.Contains("host.docker.internal"))
        {
            ConnectionString = ConnectionString.Replace("host.docker.internal", "127.0.0.1");
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _container.StopAsync().Wait();
        }
    }
}
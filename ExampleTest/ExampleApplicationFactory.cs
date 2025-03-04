using Example.Context;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Testcontainers.MsSql;

namespace ExampleTest;

public class ExampleApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _db = new MsSqlBuilder()
           .WithAutoRemove(true)
           .WithPassword("yourStrong(!)Password")
           .Build();

    private const string DatabaseName = "Example";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.Remove(services.Single(service => typeof(DbContextOptions<ExampleContext>) == service.ServiceType));
            services.AddDbContext<ExampleContext>(options => options.UseSqlServer(_db.GetConnectionString().Replace("Database=master", $"Database={DatabaseName}")));
        });
    }

    public async ValueTask InitializeAsync()
    {
        await _db.StartAsync();
        await _db.ExecScriptAsync($"CREATE DATABASE {DatabaseName}");
    }

    public new async Task DisposeAsync()
    {
        await _db.StopAsync();
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

public class IntegrationTestFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class where TDbContext : DbContext
{
    private readonly PostgreSqlContainer _postgres =
            new PostgreSqlBuilder()
                     .WithImage("postgres:15-alpine")
                     .WithResourceMapping("schema.sql", "/docker-entrypoint-initdb.d/")
                     .Build();

    public async Task InitializeAsync() => await _postgres.StartAsync();

    public new async Task DisposeAsync() => await _postgres.DisposeAsync();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connString = _postgres.GetConnectionString();
        //Console.WriteLine("TC ConnectionString="+connString);
        builder.ConfigureTestServices(services =>
        {
            services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<TDbContext>) == service.ServiceType));
            services.AddDbContext<TDbContext>(options => { options.UseNpgsql(connString); });
        });
    }
}
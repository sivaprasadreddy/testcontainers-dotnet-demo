namespace BookmarksApi.Tests;

public sealed class IntegrationTestFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : Program
    where TDbContext : DbContext
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .WithResourceMapping("schema.sql", "/docker-entrypoint-initdb.d/")
        .Build();

    Task IAsyncLifetime.InitializeAsync() => _postgreSqlContainer.StartAsync();

    Task IAsyncLifetime.DisposeAsync() => _postgreSqlContainer.DisposeAsync().AsTask();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove(services.Single(service => typeof(DbContextOptions<TDbContext>) == service.ServiceType));
            services.AddDbContext<TDbContext>(options => options.UseNpgsql(_postgreSqlContainer.GetConnectionString()));
        });
    }
}
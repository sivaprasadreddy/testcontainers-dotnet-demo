using Testcontainers.PostgreSql;

namespace BookmarksApi.Tests;

public sealed class BookmarkServiceTest : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .Build();

    public Task InitializeAsync()
    {
        return _postgres.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _postgres.DisposeAsync().AsTask();
    }

    [Fact]
    public void ShouldReturnTwoBookmarks()
    {
        // Given
        var bookmarkService = new BookmarkService(new DbConnectionProvider(_postgres.GetConnectionString()));

        // When
        bookmarkService.Create(new Bookmark(1, "SivaLabs Blog", "https://sivalabs.in"));
        bookmarkService.Create(new Bookmark(2, "Testcontainers", "https://testcontainers.com"));
        var bookmarks = bookmarkService.GetBookmarks();

        // Then
        Assert.Equal(2, bookmarks.Count());
    }
}
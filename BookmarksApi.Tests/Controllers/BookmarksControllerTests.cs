namespace BookmarksApi.Tests.Controllers;

public sealed class IntegrationTest : IClassFixture<IntegrationTestFactory<Program, DatabaseContext>>
{
    private readonly IntegrationTestFactory<Program, DatabaseContext> _factory;

    public IntegrationTest(IntegrationTestFactory<Program, DatabaseContext> factory) => _factory = factory;

    [Fact]
    public async Task should_return_bookmarks_on_http_get()
    {
        using var client = _factory.CreateClient();

        using var response = await client.GetAsync("/api/bookmarks");

        var responseJson = await response.Content.ReadAsStringAsync();

        var bookmarks = JsonSerializer.Deserialize<List<Bookmark>>(responseJson);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(bookmarks);
        Assert.Contains(bookmarks, bookmark => "foo".Equals(bookmark.Title));
        Assert.Contains(bookmarks, bookmark => "bar".Equals(bookmark.Title));
    }

    [Fact]
    public async Task should_return_bookmarks_on_get_all()
    {
        using var scope = _factory.Services.CreateScope();

        var bookmarkController = scope.ServiceProvider.GetRequiredService<BookmarkController>();

        var bookmarks = await bookmarkController.GetAll();

        Assert.NotNull(bookmarks.Value);
        Assert.Contains(bookmarks.Value, bookmark => "foo".Equals(bookmark.Title));
        Assert.Contains(bookmarks.Value, bookmark => "bar".Equals(bookmark.Title));
    }
}
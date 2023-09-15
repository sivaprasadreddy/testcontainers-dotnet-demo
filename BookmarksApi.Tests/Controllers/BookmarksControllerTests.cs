using BookmarksApi.Data;
using Microsoft.AspNetCore.Mvc.Testing;

public class IntegrationTest : IClassFixture<IntegrationTestFactory<Program, DatabaseContext>>
{
    private readonly IntegrationTestFactory<Program, DatabaseContext> _factory;

    public IntegrationTest(IntegrationTestFactory<Program, DatabaseContext> factory) => _factory = factory;


    [Fact]
    public async Task Should_return_bookmarks_on_http_get()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/bookmarks");

        response.EnsureSuccessStatusCode();
    }
}
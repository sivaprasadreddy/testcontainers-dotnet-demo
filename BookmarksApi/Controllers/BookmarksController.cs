using Microsoft.AspNetCore.Mvc;

namespace BookmarksApi.Controllers;

[ApiController]
[Route("api/bookmarks")]
public class BookmarkController : ControllerBase
{
    private readonly BookmarkService _bookmarkService;
    private readonly ILogger<BookmarkController> _logger;

    public BookmarkController(BookmarkService bookmarkService, ILogger<BookmarkController> logger)
    {
        _bookmarkService = bookmarkService;
        _logger = logger;
    }

    [HttpGet(Name = "GetAllBookmarks")]
    public IEnumerable<Bookmark> GetAll()
    {
        return _bookmarkService.GetBookmarks();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Bookmark bookmark)
    {
         await Task.Run(()=> _bookmarkService.Create(bookmark));
        return Ok();
    }
}

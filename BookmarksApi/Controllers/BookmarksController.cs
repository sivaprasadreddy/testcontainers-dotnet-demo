using BookmarksApi.Data;
using BookmarksApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookmarksApi.Controllers;

[ApiController]
[Route("api/bookmarks")]
public class BookmarkController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    public BookmarkController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [HttpGet(Name = "GetAllBookmarks")]
    public async Task<ActionResult<IEnumerable<Bookmark>>> GetAll()
    {
        return await _databaseContext.Bookmarks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Bookmark>> GetBookmark(int id)
    {
        var bookmark = await _databaseContext.Bookmarks.FindAsync(id);

        if (bookmark == null)
        {
            return NotFound();
        }

        return bookmark;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Bookmark bookmark)
    {
        _databaseContext.Bookmarks.Add(bookmark);
        await _databaseContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, Bookmark bookmark)
    {
        if (id != bookmark.Id)
        {
            return BadRequest();
        }

        _databaseContext.Entry(bookmark).State = EntityState.Modified;
        await _databaseContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var bookmark = await _databaseContext.Bookmarks.FindAsync(id);

        if (bookmark == null)
        {
            return NotFound();
        }

        _databaseContext.Bookmarks.Remove(bookmark);
        await _databaseContext.SaveChangesAsync();

        return NoContent();
    }
}

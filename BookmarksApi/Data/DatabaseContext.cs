namespace BookmarksApi.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Bookmark> Bookmarks { get; set; }
}
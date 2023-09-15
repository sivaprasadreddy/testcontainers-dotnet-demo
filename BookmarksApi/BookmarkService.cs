namespace BookmarksApi;

public sealed class BookmarkService
{
    private readonly DbConnectionProvider _dbConnectionProvider;

    public BookmarkService(DbConnectionProvider dbConnectionProvider)
    {
        _dbConnectionProvider = dbConnectionProvider;
        CreateBookmarksTable();
    }

    public IEnumerable<Bookmark> GetBookmarks()
    {
        IList<Bookmark> bookmarks = new List<Bookmark>();

        using var connection = _dbConnectionProvider.GetConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id, title, url FROM bookmarks";
        command.Connection?.Open();

        using var dataReader = command.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt64(0);
            var title = dataReader.GetString(1);
            var url = dataReader.GetString(2);
            bookmarks.Add(new Bookmark(id, title, url));
        }

        return bookmarks;
    }

    public void Create(Bookmark bookmark)
    {
        using var connection = _dbConnectionProvider.GetConnection();
        using var command = connection.CreateCommand();

        var id = command.CreateParameter();
        id.ParameterName = "@id";
        id.Value = bookmark.Id;

        var title = command.CreateParameter();
        title.ParameterName = "@title";
        title.Value = bookmark.Title;

        var url = command.CreateParameter();
        url.ParameterName = "@url";
        url.Value = bookmark.Url;

        command.CommandText = "INSERT INTO bookmarks (id, title, url) VALUES(@id, @title, @url)";
        command.Parameters.Add(id);
        command.Parameters.Add(title);
        command.Parameters.Add(url);
        command.Connection?.Open();
        command.ExecuteNonQuery();
    }

    private void CreateBookmarksTable()
    {
        using var connection = _dbConnectionProvider.GetConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS bookmarks (id BIGINT NOT NULL, title VARCHAR NOT NULL, url VARCHAR NOT NULL, PRIMARY KEY (id))";
        command.Connection?.Open();
        command.ExecuteNonQuery();
    }
}
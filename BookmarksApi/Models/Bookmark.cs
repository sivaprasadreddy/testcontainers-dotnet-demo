namespace BookmarksApi.Models;

[Table("bookmarks")]
public class Bookmark
{
    [Column("id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Column("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [Column("url")]
    [JsonPropertyName("url")]
    public string Url { get; set; }
}
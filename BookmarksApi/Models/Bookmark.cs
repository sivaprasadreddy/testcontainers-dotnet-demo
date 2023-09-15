using System.ComponentModel.DataAnnotations.Schema;

namespace BookmarksApi.Models
{
  [Table("bookmarks")]
  public class Bookmark
  {
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("url")]
    public string Url { get; set; }
  }
}
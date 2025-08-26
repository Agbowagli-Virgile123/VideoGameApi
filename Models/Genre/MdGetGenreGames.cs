using System.ComponentModel.DataAnnotations.Schema;
using VideoGameApi.Models.VideoGame;

namespace VideoGameApi.Models.Genre
{
    [NotMapped]
    public class MdGetGenreGames
    {
        public string? GenreId { get; set; }
        public List<MdGetVideoGame>? VideoGames { get; set; }
    }
}

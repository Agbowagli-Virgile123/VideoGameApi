using System.ComponentModel.DataAnnotations.Schema;
using VideoGameApi.Models.DatabaseModels;

namespace VideoGameApi.Models.VideoGame
{
    [NotMapped]
    public class MdGameGenres
    {
        public string? GameVideoId { get; set; }
        public List<Genre>? Genres { get; set; } = new List<Genre>();
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using VideoGameApi.Models.VideoGame;

namespace VideoGameApi.Models.Developer
{
    [NotMapped]
    public class MdGetDeveloperGames
    {
        public string? DeveloperId { get; set; }
        public List<MdGetVideoGame>? Games { get; set; }
    }
}

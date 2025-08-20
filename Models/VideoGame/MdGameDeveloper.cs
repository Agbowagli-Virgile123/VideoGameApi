using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameApi.Models.VideoGame
{
    [NotMapped]
    public class MdGameDeveloper
    {
        public string? GameId { get; set; } 
        public string? DeveloperId { get; set; }
        public string? DeveloperName { get; set; }
    }
}

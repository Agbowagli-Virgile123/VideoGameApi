using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameApi.Models.VideoGame
{
    [NotMapped]
    public class MdVideoGameDetails
    {
        public string? VideoGameId { get; set; }
        public string? GameDetailsId { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameApi.Models.VideoGame
{
    [NotMapped]
    public class MdGetVideoGame
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Platform { get; set; }
    }
}

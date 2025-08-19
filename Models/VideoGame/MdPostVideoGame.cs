using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameApi.Models.VideoGame
{
    [NotMapped]
    public class MdPostVideoGame
    {
        public string? Id { get; set; }
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Platform { get; set; }
        [Required]
        public string? DeveloperId { get; set; }

        [Required]
        public string? PublisherId { get; set; }

        public string? Description { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public List<string>? GenreIds { get; set; }

    }
}

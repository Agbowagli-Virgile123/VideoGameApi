using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameApi.Models.Genre
{
    [NotMapped]
    public class MdGetGenres
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}

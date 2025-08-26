using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameApi.Models.Genre
{
    [NotMapped]
    public class MdPostGenre
    {
        public string? Name { get; set; }
    }
}

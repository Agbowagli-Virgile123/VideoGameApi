using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameApi.Models.VideoGame
{
    [NotMapped]
    public class MdGamePublisher
    {
        public string? GameId { get; set; }
        public string? PublisherId { get; set; }
        public string? PublisherName { get; set; }
    }
}

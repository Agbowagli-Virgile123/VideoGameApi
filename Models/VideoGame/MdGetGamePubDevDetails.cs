using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameApi.Models.VideoGame
{
    [NotMapped]
    public class MdGetGamePubDevDetails
    {
        public string? VideoGameId { get; set; }
        public string? GameDetailsId { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? PublisherId { get; set; }
        public string? PublisherName { get; set; }
        public string? DeveloperId { get; set; }
        public string? DeveloperName { get; set; }
       
    }
}

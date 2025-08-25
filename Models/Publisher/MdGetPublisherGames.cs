using VideoGameApi.Models.VideoGame;

namespace VideoGameApi.Models.Publisher
{
    public class MdGetPublisherGames
    {
        public string? PublisherId { get; set; }
        public List<MdGetVideoGame>? Games { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace VideoGameApi.Models.DatabaseModels
{
    public class Genre
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        [JsonIgnore]
        public List<VideoGame>? VideoGames { get; set; }
    }
}

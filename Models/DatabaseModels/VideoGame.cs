namespace VideoGameApi.Models.DatabaseModels
{
    public class VideoGame
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Platform { get; set; }
        public string? DeveloperId  { get; set; }
        public virtual Developer? Developer { get; set; }
        public string? PublisherId { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public VideoGameDetails? VideoGameDetails { get; set; }
        public List<Genre>? Genres { get; set; } = new List<Genre>();
    }
}

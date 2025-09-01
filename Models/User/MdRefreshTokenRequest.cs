namespace VideoGameApi.Models.User
{
    public class MdRefreshTokenRequest
    {
        public Guid UserId { get; set; }
        public string? RefreshToken { get; set; }
    }
}

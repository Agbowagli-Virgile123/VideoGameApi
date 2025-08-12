using VideoGameApi.Models;

namespace VideoGameApi.Interfaces
{
    public interface IVideoGame
    {

        Task<List<VideoGame>> GetAllVideoGamesAsync();
        Task<VideoGame> GetSingleGameAsync(string gameId);
        Task<MdResponse> CreateGameAsync(VideoGame newGame);
        Task<MdResponse> UpdateGameAsync(string gameId, VideoGame updatedGame);
        Task<MdResponse> DeleteGameAsync(string gameId);

    }
}

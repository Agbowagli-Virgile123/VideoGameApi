using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.VideoGame;

namespace VideoGameApi.Interfaces
{
    public interface IVideoGame
    {
        Task<List<MdGetVideoGame>> GetAllVideoGamesAsync();
        Task<MdGetVideoGame> GetSingleGameAsync(string gameId);
        Task<MdResponse> CreateGameAsync(MdPostVideoGame newGame);
        Task<MdResponse> UpdateGameAsync(string gameId, VideoGame updatedGame);
        Task<MdResponse> DeleteGameAsync(string gameId);

    }
}

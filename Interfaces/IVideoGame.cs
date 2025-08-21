using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.VideoGame;

namespace VideoGameApi.Interfaces
{
    public interface IVideoGame
    {
        Task<List<MdGetVideoGame>> GetAllVideoGamesAsync();
        Task<MdGetVideoGame> GetSingleGameAsync(string gameId);
        Task<MdVideoGameDetails?> GetGameDetails(string gameId);
        Task<MdGameDeveloper> GetGameDeveloper(string gameId);
        Task<MdGamePublisher> GetGamePublisher(string gameId);
        Task<MdGetGamePubDevDetails> GetGamePubDevDetails(string gameId);
        Task<List<Genre>> Genres(string gameId);
        Task<MdGameGenres> GetGameGenres(string gameId);
        Task<MdResponse> CreateGameAsync(MdPostVideoGame newGame);
        Task<MdResponse> UpdateGameAsync(string gameId, VideoGame updatedGame);
        Task<MdResponse> DeleteGameAsync(string gameId);


    }
}

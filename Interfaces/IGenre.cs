using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.Genre;

namespace VideoGameApi.Interfaces
{
    public interface IGenre
    {
        Task<List<MdGetGenres>> GetAllGenres();
        Task<MdGetGenres> GetGenresById(string id);
        Task<List<MdGetGenreGames>> GetAllGenreGames();
        Task<MdGetGenreGames?> GetGenreGames(string id);
        Task<MdResponse> CreateGenre(MdPostGenre genre);
        Task<MdResponse> UpdateGenre(string genreId, MdPostGenre genre);
        Task<MdResponse> DeleteGenre(string genreId);
    }
}

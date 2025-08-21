using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.Developer;

namespace VideoGameApi.Interfaces
{
    public interface IDeveloper
    {
        Task<List<Developer>> GetAllDevelopers();
        Task<Developer> GetDeveloperById(string devId);
        Task<MdGetDeveloperGames> GetDevGames(string devId);
    }
}

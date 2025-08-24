using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.Developer;

namespace VideoGameApi.Interfaces
{
    public interface IDeveloper
    {
        Task<List<Developer>> GetAllDevelopers();
        Task<Developer> GetDeveloperById(string devId);
        Task<List<MdGetDeveloperGames>> GetAllDevGames();
        Task<MdGetDeveloperGames> GetDevGames(string devId);
        Task<MdResponse> CreateDeveloper(Developer developer);
        Task<MdResponse> UpdateDeveloper(string developerId, string Name);
        Task<MdResponse> DeleteDeveloper(string developerId);

    }
}

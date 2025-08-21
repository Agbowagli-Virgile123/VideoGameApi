using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.ComponentModel;
using VideoGameApi.Data;
using VideoGameApi.Interfaces;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.Developer;
using VideoGameApi.Models.VideoGame;

namespace VideoGameApi.Services
{
    public class DeveloperServices : IDeveloper
    {
        public readonly VideoGameDbContext _context;

        public DeveloperServices(VideoGameDbContext context)
        {
            _context = context;
        }

        //Get all Developers
        public async Task<List<Developer>> GetAllDevelopers()
        {
            var developers = await _context.Set<Developer>().ToListAsync();

            return developers;
        }

        //Get developer by Id
        public async Task<Developer> GetDeveloperById(string devId)
        {
            var developer = await _context.Set<Developer>().FindAsync(devId);
            return developer;
        }

        //Get All the games under developers
        //public async Task<List<MdGetDeveloperGames>> GetAllDevGames()
        //{
        //    var devGames = await _context.Set<Developer>()
        //                        .Select( _context.VideoGames.Where( d => d.Developer ==  ) )
        //}

        //Get Games o a developer
        public async Task<MdGetDeveloperGames> GetDevGames(string devId)
        {
            var dev = await _context.Set<Developer>().FindAsync(devId);

            var games = await _context.VideoGames.Where(g => g.Developer == dev)
                            .Select(g => new MdGetVideoGame
                            {
                                Id = g.Id,
                                Title = g.Title,
                                Platform = g.Platform,
                            }).ToListAsync();


            var devGames = new MdGetDeveloperGames
            {
                DeveloperId = dev?.Id,
                Games = games
            };

            return devGames;
                         
        }
    }
}

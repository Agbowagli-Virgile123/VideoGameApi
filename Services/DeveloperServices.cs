using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using VideoGameApi.Data;
using VideoGameApi.Interfaces;
using VideoGameApi.Models;
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
        public async Task<Developer?> GetDeveloperById(string devId)
        {
            var developer = await _context.Set<Developer>().FindAsync(devId);

            if(developer == null)
            {
                return new Developer { Name = null!};
            }

            return developer;
        }

        //Get All the games under developers
        public async Task<List<MdGetDeveloperGames>> GetAllDevGames()
        {
            var devGames = await _context.Set<Developer>()

                .GroupJoin(_context.VideoGames,
                    d => d.Id,
                    vg => vg.DeveloperId,
                    (d, games) => new MdGetDeveloperGames
                    {
                        DeveloperId = d.Id,
                        Games = games.Select(g => new MdGetVideoGame
                        {
                            Id = g.Id,
                            Title = g.Title,
                            Platform = g.Platform,

                        }).ToList()

                    }).ToListAsync();

            return devGames;

        }

        //Get Games of a developer
        public async Task<MdGetDeveloperGames> GetDevGames(string devId)
        {
            var dev = await _context.Set<Developer>()
                .Where(d => d.Id == devId).FirstOrDefaultAsync();

            if(dev == null)
            {
                return new MdGetDeveloperGames();
            }

            var games = await _context.VideoGames
                        .AsNoTracking()
                        .Where(g => g.Developer == dev)
                        .Select(g => new MdGetVideoGame
                        {
                            Id = g.Id,
                            Title = g.Title,
                            Platform = g.Platform,

                        }).ToListAsync();

            var devGames = new MdGetDeveloperGames
            {
                DeveloperId = dev.Id,
                Games = games
            };

            return devGames;
                         
        }
    
    
        //Create a new developer
        public async Task<MdResponse> CreateDeveloper(Developer developer)
        {

            MdResponse response = new MdResponse { ResponseCode = 0 };

            if(developer == null || string.IsNullOrEmpty(developer.Name))
            {
                response.ResponseMessage = "Developer name is required";
                return response;

            }

            developer.Id = Guid.NewGuid().ToString();

            await _context.Set<Developer>().AddAsync(developer);
            await _context.SaveChangesAsync();

            response.ResponseCode = 1;
            response.ResponseMessage = "Developer Created Successfully";

            return response;

        }
    
        //Update Developer 
        public async Task<MdResponse> UpdateDeveloper(string developerId, string Name)
        {
            MdResponse response = new MdResponse { ResponseCode = 0};
            //var dev = await _context.Set<Developer>().AnyAsync(d => d.Id == developerId);
            var devData = await _context.Set<Developer>().FindAsync(developerId);

            if (devData == null) 
            {
                response.ResponseMessage = "Developer not found";
                return response;
            }

            if(string.IsNullOrEmpty(Name))
            {
                response.ResponseMessage = "Name is required";
                return response;
            }

            devData.Name = Name;
            await _context.SaveChangesAsync();

            response.ResponseCode = 1;
            response.ResponseMessage = "Developer updated successfully";
            return response;
        }

        
        //Delete Developer 
        public async Task<MdResponse> DeleteDeveloper(string developerId)
        {
            MdResponse response = new MdResponse { ResponseCode = 0};
            var dev = await _context.Set<Developer>().FindAsync(developerId);


            if (dev is null) 
            {
                response.ResponseMessage = "Developer not found";
                return response;
            }

            _context.Set<Developer>().Remove(dev);
            await _context.SaveChangesAsync();

            response.ResponseCode = 1;
            response.ResponseMessage = "Developer deleted successfully";
            return response;
        }

        

    }
}

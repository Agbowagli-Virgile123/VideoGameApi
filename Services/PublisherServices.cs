using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using VideoGameApi.Data;
using VideoGameApi.Interfaces;
using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.Publisher;
using VideoGameApi.Models.VideoGame;

namespace VideoGameApi.Services
{
    public class PublisherServices : IPublisher
    {
        private readonly VideoGameDbContext _context;

        public PublisherServices(VideoGameDbContext context)
        {
            _context = context;
        }

        //Get all publisher
        public async Task<List<Publisher>> GetAllPublishers()
        {
            var publishers = await _context.Set<Publisher>().ToListAsync();

            return publishers;
        }

        //Get publisher by Id
        public async Task<Publisher?> GetPublisherById(string publisherId)
        {
            if (string.IsNullOrEmpty(publisherId))
            {
                return new Publisher { Name = null!};
            }

            var publisher = await _context.Set<Publisher>().FindAsync(publisherId);

            return publisher;
        }
         
        //Get All the games under a publishers
        public async Task<List<MdGetPublisherGames>> GetAllPubGames()
        {
            var pubGames = await _context.Set<Publisher>()
                            .GroupJoin(_context.VideoGames,
                                p => p.Id,
                                g => g.PublisherId,
                                (p, games) => new MdGetPublisherGames
                                {
                                    PublisherId = p.Id,
                                    Games = games.Select( g => new MdGetVideoGame
                                    {
                                        Id = g.Id,
                                        Title = g.Title,
                                        Platform = g.Platform,

                                    }).ToList(),

                                }).ToListAsync();
            return pubGames;
        }

        //Get all game under publisher 
        public async Task<MdGetPublisherGames> GetPubGames(string pubId)
        {
            
            if (string.IsNullOrEmpty(pubId))
            {
                return new MdGetPublisherGames();
            }

            var pub = await _context.Set<Publisher>().FindAsync(pubId);

            if (pub == null)
            {
                return new MdGetPublisherGames{ Games = new List<MdGetVideoGame>() };
            }


            var games = await _context.VideoGames
                    .Where(g => g.Publisher == pub)
                    .Select(v => new MdGetVideoGame
                    {
                        Id = v.Id,
                        Title = v.Title,
                        Platform = v.Platform,

                    }).ToListAsync();

            var pubGames = new MdGetPublisherGames
            {
                PublisherId = pub.Id,
                Games = games
            };

            return pubGames;
        }

        //Create Publisher
        public async Task<MdResponse> CreatePublisher(Publisher publisher)
        {
            MdResponse response = new MdResponse { ResponseCode = 0};

            if(publisher == null || string.IsNullOrEmpty(publisher.Name))
            {
                response.ResponseMessage = "Name is required";
                return response;
            }

            publisher.Id = Guid.NewGuid().ToString();
            await _context.Set<Publisher>().AddAsync(publisher);
            await _context.SaveChangesAsync();

            response.ResponseCode = 1;
            response.ResponseMessage = "Publisher created successfully";
            return response;
        }

        //Update Publisher
        public async Task<MdResponse> UpdatePublisher(string publisherId, string Name)
        {
            MdResponse response = new MdResponse { ResponseCode = 0 };

            if (string.IsNullOrEmpty(publisherId))
            {
                response.ResponseMessage = "Pbblisher Id is required";
                return response;
            }

            var pub = await _context.Set<Publisher>().FindAsync(publisherId);

            if (pub == null)
            {
                response.ResponseMessage = "Invalid Publisher";
                return response;    
            }

            pub.Name = Name;
            await _context.SaveChangesAsync();
            response.ResponseCode = 1;
            response.ResponseMessage = "Publisher updated successfully";
            return response;

        }
        
        
        //Delete publisher
        public async Task<MdResponse> DeletePublisher(string publisherId)
        {
            MdResponse response = new MdResponse { ResponseCode = 0 };

            if (string.IsNullOrEmpty(publisherId))
            {
                response.ResponseMessage = "Pbblisher Id is required";
                return response;
            }

            var pub = await _context.Set<Publisher>().FindAsync(publisherId);

            if (pub == null)
            {
                response.ResponseMessage = "Invalid Publisher";
                return response;    
            }

           
            _context.Set<Publisher>().Remove(pub);
            await _context.SaveChangesAsync();
            response.ResponseCode = 1;
            response.ResponseMessage = "Publisher deleted successfully";
            return response;

        }
    }


}

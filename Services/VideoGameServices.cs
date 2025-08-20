using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;
using VideoGameApi.Interfaces;
using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.VideoGame;

namespace VideoGameApi.Services
{
    public class VideoGameServices : IVideoGame
    {

        private readonly VideoGameDbContext _context;

        public VideoGameServices(VideoGameDbContext context)
        {
            _context = context;
        }

        //Get all video games
        public async Task<List<MdGetVideoGame>> GetAllVideoGamesAsync()
        {
            var videoGames = await _context.VideoGames
                .Select(g => new MdGetVideoGame
                {
                    Id = g.Id,
                    Title = g.Title,
                    Platform = g.Platform
                }).ToListAsync();
              
            return videoGames;
        }

        //Get a single video game by ID
        public async Task<MdGetVideoGame> GetSingleGameAsync(string gameId)
        {

            var game = await _context.VideoGames
                .Where(g => g.Id == gameId)
                .Select(g => new MdGetVideoGame
                {
                    Id = g.Id,
                    Title = g.Title,
                    Platform = g.Platform
                })
                .SingleOrDefaultAsync();


            return game ?? new();
        }


        //Get Video game details by game Id
        public async Task<MdVideoGameDetails?> GetGameDetails(string gameId)
        {

            var hasDetails = await _context.VideoGames.Where(g => g.Id == gameId && g.VideoGameDetails != null).AnyAsync();

            if (!hasDetails)
            {
                return new MdVideoGameDetails(); // No details found for the game
            }

            //Check if the game exists
            var game = await _context.VideoGames
                .Where(g => g.Id == gameId)
                .Select(g => new MdVideoGameDetails
                {
                    VideoGameId = g.Id,
                    GameDetailsId = g.VideoGameDetails != null ? g.VideoGameDetails.Id : null,
                    Description = g.VideoGameDetails != null ? g.VideoGameDetails.Description : null,
                    ReleaseDate = g.VideoGameDetails.ReleaseDate
                }).FirstOrDefaultAsync();

          
                return game;

        }

        //Get the developer of a video game
        public async Task<MdGameDeveloper?> GetGameDeveloper(string gameId)
        {
            var developer = await _context.VideoGames
                .Where(g => g.Id == gameId)
                .Select(g => new MdGameDeveloper
                {
                    GameId = g.Id,
                    DeveloperId = g.Developer.Id,
                    DeveloperName = g.Developer.Name
                })
                .FirstOrDefaultAsync();

            return developer;
        }

        //Get the publisher of a video game
        public async Task<MdGamePublisher?> GetGamePublisher(string gameId)
        {
            var publisher = await _context.VideoGames
                .Where(g => g.Id == gameId)
                .Select(g => new MdGamePublisher
                {
                    GameId = g.Id,
                    PublisherId = g.Publisher.Id,
                    PublisherName = g.Publisher.Name
                })
                .FirstOrDefaultAsync();
            return publisher;
        }

        public async Task<MdGetGamePubDevDetails> GetGamePubDevDetails(string gameId)
        {
            var gameDetails = await _context.VideoGames
                .Where(g => g.Id == gameId)
                .Select(g => new MdGetGamePubDevDetails
                {
                    VideoGameId = g.Id,
                    GameDetailsId = g.VideoGameDetails != null ? g.VideoGameDetails.Id : null,
                    Description = g.VideoGameDetails != null ? g.VideoGameDetails.Description : null,
                    ReleaseDate = g.VideoGameDetails != null ? g.VideoGameDetails.ReleaseDate : default,
                    PublisherId = g.Publisher.Id,
                    PublisherName = g.Publisher.Name,
                    DeveloperId = g.Developer.Id,
                    DeveloperName = g.Developer.Name
                })
                .FirstOrDefaultAsync();
            return gameDetails ?? new MdGetGamePubDevDetails();
        }


        //Get Game genres
        public async Task<MdGameGenres> GetGameGenres(string gameId)
        {
            var genres = await _context.VideoGames
                        .Include(g => g.Genres)
                       .FirstOrDefaultAsync();

            var gameGenres = new MdGameGenres
            {
                GameVideoId = gameId,
                Genres = genres?.Genres.Select(g => new Genre { Id = g.Id, Name = g.Name }).ToList()
            };

            return gameGenres;
        }
           

        public async Task<MdResponse> CreateGameAsync(MdPostVideoGame newGame)
        {
            MdResponse response = new MdResponse { ResponseCode = 0 };
            if (newGame is null)
            {
                response.ResponseMessage = "Invalid Game Data";
                return response;
            }

            if (string.IsNullOrEmpty(newGame.Title) || string.IsNullOrEmpty(newGame.Platform) ||
                string.IsNullOrEmpty(newGame.DeveloperId) || string.IsNullOrEmpty(newGame.PublisherId))
            {
                response.ResponseMessage = "Title, Platform, DeveloperId, and PublisherId are required";
                return response;
            }

            //Check if the Developer and Publisher exist
            var dev = await _context.Set<Developer>()
                .FindAsync(newGame.DeveloperId);

            if (dev is null)
            {
                response.ResponseMessage = "Developer does not exist";
                return response;
            }

            var pub = await _context.Set<Publisher>()
                .FindAsync(newGame.PublisherId);
            if (pub is null)
            {
                response.ResponseMessage = "Publisher does not exist";
                return response;
            }

            
            //Mapping new game to the genre if GenreIds are provided
            List<Genre> genres = new List<Genre>();
            if (newGame.GenreIds is not null && newGame.GenreIds.Any())
            {
                genres = await _context.Set<Genre>()
                    .Where(gd => newGame.GenreIds.Contains(gd.Id))
                    .ToListAsync();

                if (genres.Count != newGame.GenreIds.Count)
                {
                    response.ResponseMessage = "One or more GenreIds are invalid";
                    return response;
                }
            }

            //Build the new game object
            newGame.Id = Guid.NewGuid().ToString();

            var Video = new VideoGame
            {
                Id = newGame.Id,
                Title = newGame.Title,
                Platform = newGame.Platform,
                Developer = dev,
                Publisher = pub,
                Genres = genres,
            };


            if (!string.IsNullOrEmpty(newGame.Description) || newGame.ReleaseDate != default)
            {
                Video.VideoGameDetails = new VideoGameDetails
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = newGame.Description,
                    ReleaseDate = newGame.ReleaseDate,
                    VideoGameId = Video.Id
                };
            }
            
            await _context.VideoGames.AddAsync(Video);

            await _context.SaveChangesAsync();

            response.ResponseCode = 1;
            response.ResponseMessage = "Game Created Successfully";
            return response;
        }

        public async Task<MdResponse> UpdateGameAsync(string gameId, VideoGame updatedGame)
        {
            MdResponse response = new MdResponse { ResponseCode = 0 };
            if (updatedGame is null || string.IsNullOrEmpty(gameId))
            {
                response.ResponseMessage = "Invalid Game Data";
                return response;
            }

            var existingGame = await _context.VideoGames.FindAsync(gameId);
            if (existingGame is null)
            {
                response.ResponseMessage = "Game Not Found";
                return response;
            }

            existingGame.Title = updatedGame.Title;
            existingGame.Platform = updatedGame.Platform;
            existingGame.Developer = updatedGame.Developer;
            existingGame.Publisher = updatedGame.Publisher;

            _context.VideoGames.Update(existingGame);
            await _context.SaveChangesAsync();

            response.ResponseCode = 1;
            response.ResponseMessage = "Game Updated Successfully";
            return response;
        }


        public async Task<MdResponse> DeleteGameAsync(string gameId)
        {
            MdResponse response = new MdResponse { ResponseCode = 0 };
            if (string.IsNullOrEmpty(gameId))
            {
                response.ResponseMessage = "GameId is required";
                return response;
            }
            var game = await _context.VideoGames.FindAsync(gameId);
            if (game is null)
            {
                response.ResponseMessage = "Game Not Found";
                return response;
            }
            _context.VideoGames.Remove(game);
            await _context.SaveChangesAsync();
            response.ResponseCode = 1;
            response.ResponseMessage = "Game Deleted Successfully";
            return response;
        }
    }
}

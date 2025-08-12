using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;
using VideoGameApi.Interfaces;
using VideoGameApi.Models;

namespace VideoGameApi.Services
{
    public class VideoGameServices : IVideoGame
    {

        private readonly VideoGameDbContext _context;

        public VideoGameServices(VideoGameDbContext context)
        {
            _context = context;
        }


        public async Task<List<VideoGame>> GetAllVideoGamesAsync()
        {
            var videoGames = await _context.VideoGames
                .Include(g => g.VideoGameDetails)
                .ToListAsync();

            return videoGames;
        }

        public async Task<VideoGame> GetSingleGameAsync(string gameId)
        {

            var game = await _context.VideoGames.FindAsync(gameId);

            if (game is null)
            {

                return new VideoGame();
            }



            return game;
        }

        public async Task<MdResponse> CreateGameAsync(VideoGame newGame)
        {
            MdResponse response = new MdResponse { ResponseCode = 0 };
            if (newGame is null)
            {
                response.ResponseMessage = "Invalid Game Data";
                return response;
            }


            newGame.Id = Guid.NewGuid().ToString();

            _context.VideoGames.Add(newGame);
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

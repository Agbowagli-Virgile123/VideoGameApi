using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using VideoGameApi.Data;
using VideoGameApi.Models;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
       

        private readonly VideoGameDbContext _context;


        public VideoGameController(VideoGameDbContext context) { 

            _context = context;
        
        }



        [HttpGet("GetAllVideosGames")]
        public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
        {

            var videoGames = await _context.VideoGames.ToListAsync();

            return Ok(videoGames);
        }

        [HttpGet("GetSingleGame/{gameId}")]

        public async Task<ActionResult<object>> GetSingleGame(string gameId)
        {
            MdResponse response = new MdResponse { ResponseCode = 0 };

            //var game = videoGames.Where(e => e.Id.Equals(gameId));
            //var game = videoGames.Where(e => e.Id == gameId);
            var game = await _context.VideoGames.FindAsync(gameId);

            if (game is null)
            {
                response.ResponseMessage = "No Game Found";

                return Ok(response);
            }

            return Ok(game);

        }

        [HttpPost("CreateGame")]
        public async Task<ActionResult<object>> CreateGame(VideoGame newGame)
        {
            MdResponse response = new MdResponse { ResponseCode = 0 };


            if (newGame is null)
            {
                response.ResponseMessage = "Failed to create game";
                return Ok(response);
            }

            if (string.IsNullOrEmpty(newGame.Title))
            {
                response.ResponseMessage = "Title is required";
                return Ok(response);
            }

            //Initiate the saving

            newGame.Id = Guid.NewGuid().ToString();

            await _context.VideoGames.AddAsync(newGame);

            //This the line saving data in the database

            await _context.SaveChangesAsync();

            response.ResponseCode = 1;
            response.ResponseMessage = "Saved Successfully";
            var result = new
            {
                response,
                newGame
            };

            return Ok(result);

        }

        [HttpPut("UpdateGame/{gameId}")]
        public async Task<ActionResult<MdResponse>> UpdateGame(string gameId, VideoGame updatedGame)
        {

            var response = new MdResponse { ResponseCode = 0 };

            if (string.IsNullOrEmpty(gameId))
            {
                response.ResponseMessage = "GameId is required";
                return Ok(response);
            }

            //Check if game exists
            var getGame = await _context.VideoGames.FindAsync(gameId);

            if (getGame is null)
            {

                response.ResponseMessage = "Game Not Found";
                return Ok(response);
            }

            getGame.Title = updatedGame.Title;

            await _context.SaveChangesAsync();

            response.ResponseCode = 1;
            response.ResponseMessage = "Updated Successfully";
            var result = new
            {
                response,
                getGame
            };

            return Ok(result);
        }

        [HttpDelete("DeleteGame/{gameId}")]
        public async Task<ActionResult<MdResponse>> DeleteGame(string gameId)
        {

            var getGame = await _context.VideoGames.FindAsync(gameId);

           _context.VideoGames.Remove(getGame);

            await _context.SaveChangesAsync();

            return Ok(new MdResponse
            {
                ResponseCode = 1,
                ResponseMessage = "Deleted Successfully"
            });

        }
    }
}

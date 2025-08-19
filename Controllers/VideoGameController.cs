using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using VideoGameApi.Data;
using VideoGameApi.Interfaces;
using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.VideoGame;

namespace VideoGameApi.Controllers
{
    [ApiController]
    public class VideoGameController : ControllerBase
    {
       

        private readonly IVideoGame _videoGame;


        public VideoGameController(IVideoGame videoGame) { 

            _videoGame = videoGame;
        
        }



        [HttpGet("GetAllVideoGames")]
        public async Task<ActionResult<List<MdGetVideoGame>>> GetVideoGames()
        {

            var videoGames = await _videoGame.GetAllVideoGamesAsync();

            return Ok(videoGames);
        }

        [HttpGet("GetVideoGameById/{gameId}")]

        public async Task<ActionResult<MdGetVideoGame>> GetSingleGame(string gameId)
        {
            
           var game = await _videoGame.GetSingleGameAsync(gameId);

            return Ok(game);

        }

        [HttpPost("CreateVideoGame")]
        public async Task<IActionResult> CreateGame([FromBody] MdPostVideoGame newGame)
        {
            

            var response = await _videoGame.CreateGameAsync(newGame);

            return Ok(response);
        }

        [HttpPut("UpdateGame/{gameId}")]
        public async Task<ActionResult<MdResponse>> UpdateGame(string gameId, VideoGame updatedGame)
        {
            var response = await _videoGame.UpdateGameAsync(gameId, updatedGame);

            return Ok(response);
        }

        [HttpDelete("DeleteGame/{gameId}")]
        public async Task<ActionResult<MdResponse>> DeleteGame(string gameId)
        {

            var response = await _videoGame.DeleteGameAsync(gameId);

            return Ok(response);

        }
    }
}

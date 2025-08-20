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

        [HttpGet("GetGameDetailsByGameId/{gameId}")]
        public async Task<IActionResult> GetGameDetailsByGameId(string gameId)
        {
            var gameDetails = await _videoGame.GetGameDetails(gameId);
            
            return Ok(gameDetails);
        }

        [HttpGet("GetGameDeveloper/{gameId}")]
        public async Task<IActionResult> GetGameDeveloper(string gameId)
        {
            var developer = await _videoGame.GetGameDeveloper(gameId);
            return Ok(developer);
        }

        [HttpGet("GetPublisher/{gameId}")]
        public async Task<IActionResult> GetGamePublisher(string gameId)
        {
            var publisher = await _videoGame.GetGamePublisher(gameId);

            return Ok(publisher);
        }

        [HttpGet("GetGamePubDevDetails/{gameId}")]
        public async Task<IActionResult> GetGamePubDevDetails(string gameId)
        {
            var response = await _videoGame.GetGamePubDevDetails(gameId);
            return Ok(response);
        }

        [HttpGet("GetGameGenres/{gameId}")]
        public async Task<IActionResult> GetGameGenres(string gameId)
        {
            var genres = _videoGame.GetGameGenres(gameId);
            return Ok(genres);
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

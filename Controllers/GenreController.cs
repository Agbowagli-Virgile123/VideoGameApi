using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using VideoGameApi.Interfaces;
using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.Genre;

namespace VideoGameApi.Controllers
{
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenre _genre;

        public GenreController(IGenre genre)
        {
            _genre = genre;
        }

        [HttpGet("GetAllGenres")]
        public async Task<ActionResult<List<MdGetGenres>>> GetAllGenres()
        {
            var res  = await _genre.GetAllGenres();

            return Ok(res);
        }
        [HttpGet("GetGenreById/{genreId}")]
        public async Task<ActionResult<MdGetGenres>> GetGenreById(string genreId)
        {
            var resp = await _genre.GetGenresById(genreId);

            return Ok(resp);
        }

        [HttpGet("GetAllGenreGames")]
        public async Task<ActionResult<MdGetGenreGames>> GetAllGenreGames()
        {
            var resp = await _genre.GetAllGenreGames();
            return Ok(resp);
        }

        [HttpGet("GetGenreGames/{genreId}")]
        public async Task<ActionResult<MdGetGenreGames>> GetGenreGames(string genreId)
        {
            var resp = await _genre.GetGenreGames(genreId);
            return Ok(resp);
        }

        [HttpPost("CreateGenre")]
        public async Task<ActionResult<MdResponse>> CreateGenre([FromBody] MdPostGenre genre)
        {
            var res = await _genre.CreateGenre(genre);
            return Ok(res);
        }

        [HttpPut("UpdateGenre/{genreId}")]
        public async Task<ActionResult<MdResponse>> UpdateGenre(string genreId, [FromBody]MdPostGenre genre)
        {
            var res = await _genre.UpdateGenre(genreId,genre);
            return Ok(res);
        }

        [HttpDelete("DeleteGenre/{genreId}")]
        public async Task<ActionResult<MdResponse>> DeleteGenre(string genreId)
        {
            var res = await _genre.DeleteGenre(genreId);
            return Ok(res);
        }
    }
}

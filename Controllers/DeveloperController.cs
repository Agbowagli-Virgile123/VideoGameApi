using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameApi.Interfaces;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.Developer;

namespace VideoGameApi.Controllers
{
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloper _developer;

        public DeveloperController(IDeveloper developer)
        {
            _developer = developer;
        }

        [Authorize]
        [HttpGet("GetAllDevs")]
        public async Task<ActionResult<List<Developer>>> Developers()
        {
            var response = await _developer.GetAllDevelopers();
            return Ok(response);
        }

        [HttpGet("GetDevById/{devId}")]
        public async Task<ActionResult<Developer>> GetDevById(string devId)
        {
            var resp = await _developer.GetDeveloperById(devId);
            return Ok(resp);
        }

        [HttpGet("GetAllDevGames")]
        public async Task<ActionResult<List<MdGetDeveloperGames>>> GetAllDevGames()
        {
            var games = await _developer.GetAllDevGames();
            return Ok(games);
        }

        [HttpGet("GetDevGames/{devId}")]
        public async Task<ActionResult<List<MdGetDeveloperGames>>> GetDevGames(string devId)
        {
            var resp = await _developer.GetDevGames(devId);
            return Ok(resp);
        }

        [HttpPost("CreateDeveloper")]
        public async Task<IActionResult> CreateDeveloper([FromBody]Developer developer)
        {
            var resp = await _developer.CreateDeveloper(developer);
            return Ok(resp);
        }

        [HttpPut("UpdateDeveloper/{developerId}")]
        public async Task<IActionResult> UpdateDeveloper(string developerId, [FromBody]string Name)
        {
            var resp = await _developer.UpdateDeveloper(developerId, Name);
            return Ok(resp);
        }

        [HttpDelete("DeleteDeveloper/{developerId}")]
        public async Task<IActionResult> DeleteDeveloper(string developerId)
        {
            var resp = await _developer.DeleteDeveloper(developerId);
            return Ok(resp);
        }


    }
}

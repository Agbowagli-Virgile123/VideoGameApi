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

        [HttpGet("GetDevGames/{devId}")]
        public async Task<ActionResult<List<MdGetDeveloperGames>>> GetDevGames(string devId)
        {
            var resp = await _developer.GetDevGames(devId);
            return Ok(resp);
        }
    }
}

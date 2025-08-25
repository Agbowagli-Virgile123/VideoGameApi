using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using VideoGameApi.Interfaces;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.Publisher;

namespace VideoGameApi.Controllers
{
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisher _publisher;

        public PublisherController(IPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpGet("GetPubs")]
        public async Task<ActionResult<List<Publisher>>> GetAllPublishers()
        {
            var pubs = await _publisher.GetAllPublishers();
            return Ok(pubs);
        }

        [HttpGet("GetPublisherById/{publisherId}")]
        public async Task<ActionResult<Publisher>> GetPublisher(string publisherId)
        {
            var pub = await _publisher.GetPublisherById(publisherId);
            return Ok(pub);
        }

        [HttpGet("GetAllPubGames")]
        public async Task<ActionResult<List<MdGetPublisherGames>>> GetAllPubGames()
        {
            var resp = await _publisher.GetAllPubGames();
            return Ok(resp);
        }

        [HttpGet("GetPubGames/{pubId}")]
        public async Task<ActionResult<MdGetPublisherGames>> GetPubGames(string pubId)
        {
            var pubGames = await _publisher.GetPubGames(pubId);
            return Ok(pubGames);
        }

        [HttpPost("CreatePublisher")]
        public async Task<IActionResult> CreatePublisher([FromBody] Publisher publisher)
        {
            var resp = await _publisher.CreatePublisher(publisher);
            return Ok(resp);
        }

        [HttpPut("UpdatePublisher/{pubId}")]
        public async Task<IActionResult> UpdatePublisher(string pubId, [FromBody] string publisher)
        {
            var resp = await _publisher.UpdatePublisher(pubId, publisher);
            return Ok(resp);
        }

        [HttpDelete("DeletePublisher/{pubId}")]
        public async Task<IActionResult> DeletePublisher(string pubId)
        {
            var resp = await _publisher.DeletePublisher(pubId);
            return Ok(resp);
        }
    }
}

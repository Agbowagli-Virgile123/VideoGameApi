using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using VideoGameApi.Interfaces;
using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.User;

namespace VideoGameApi.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUser _user;
        
        public AuthController(IUser user)
        {
            _user = user;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<(MdResponse, User)>> Register([FromBody] MdUser request)
        {
            var ( resp , user) = await _user.RegisterUser(request);

            return Ok(new
            {
                Response = resp,
                User = user
            });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<object>> LogIn([FromBody] MdUser cred)
        {
            var ( resp, refreshToken ,token, user ) = await _user.LogInUser(cred);
            return Ok(new
            {
                Response = resp,
                AccessToken = token,
                RefreshToken = refreshToken,
                User = user
            });
        }


        [Authorize]
        [HttpGet("AuthenticatedOnlyEndpoint")]
        public ActionResult<string> AutenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }

        [Authorize(Roles = "Admin, Developer")]
        [HttpGet("AdminOnlyEndpoint")]
        public ActionResult<string> AdminOnlyEndpoint()
        {
            return Ok("You are an admin or a developer");
        }
    }
}

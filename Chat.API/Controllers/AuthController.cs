using Chat.Application.Command.Auth.Login;
using Chat.Application.Command.Auth.Logout;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ISender _sender) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var result = await _sender.Send(loginCommand);
            return Ok(result);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutCommand logoutCommand)
        {
            if (string.IsNullOrEmpty(logoutCommand.token))
                return BadRequest("Invalid Refresh Token");
            await _sender.Send(logoutCommand);
            return Ok();
        }
    }
}

using Chat.Application.Command.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ISender _sender) : ControllerBase
    {
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var result = await _sender.Send(loginCommand);
            return Ok(result);
        }
    }
}

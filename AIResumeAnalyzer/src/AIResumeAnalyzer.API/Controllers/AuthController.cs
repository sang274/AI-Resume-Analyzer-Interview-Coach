using AIResumeAnalyzer.Application.Features.Auth.Commands.Login;
using AIResumeAnalyzer.Application.Features.Auth.Commands.Refresh_Token;
using AIResumeAnalyzer.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIResumeAnalyzer.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var result =
                await _mediator.Send(command);

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult TestAccessToken()
        {
            return Ok("JWT Works!");
        }
    }
}

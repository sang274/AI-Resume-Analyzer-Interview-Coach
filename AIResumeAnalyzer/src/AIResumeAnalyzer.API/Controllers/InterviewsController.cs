using AIResumeAnalyzer.Application.Features.Interview.Question.Commands.Create;
using AIResumeAnalyzer.Application.Features.Interview.Session.Commands.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIResumeAnalyzer.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/interviews")]
    public class InterviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InterviewsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateQuestions(
            GenerateInterviewQuestionsCommand command)
        {
            var sessionId =
                await _mediator.Send(command);

            return Ok(new
            {
                SessionId = sessionId
            });
        }

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetSessionById(Guid sessionId)
        {
            var result = await _mediator.Send(new GetInterviewSessionQuery(sessionId));

            return Ok(result);
        }
    }
}

using AIResumeAnalyzer.Application.Features.Interview.Answer.Command;
using AIResumeAnalyzer.Application.Features.Interview.Answer.DTO;
using AIResumeAnalyzer.Application.Features.Interview.Question.Commands.Create;
using AIResumeAnalyzer.Application.Features.Interview.Session.Commands.Finish;
using AIResumeAnalyzer.Application.Features.Interview.Session.Commands.Queries.GetInterviewSessionById;
using AIResumeAnalyzer.Application.Features.Interview.Session.Commands.Queries.GetInterviewSessions;
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

        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetMySessions()
        {
            var result = await _mediator.Send(new GetListMyInterviewSessionsQuery());

            return Ok(result);
        }

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetSessionById(Guid sessionId)
        {
            var result = await _mediator.Send(new GetInterviewSessionByIdQuery(sessionId));

            return Ok(result);
        }

        [HttpPost("questions/{questionId}/answer")]
        public async Task<IActionResult> SubmitAnswer(Guid questionId, [FromBody] SubmitInterviewAnswerRequest request)
        {
            var result = await _mediator.Send(
                new SubmitInterviewAnswerCommand(
                    questionId,
                    request.Answer));

            return Ok(result);
        }

        [HttpPost("{sessionId}/finish")]
        public async Task<IActionResult> FinishInterview(Guid sessionId)
        {
            var result = await _mediator.Send(new FinishInterviewSessionCommand(sessionId));

            return Ok(result);
        }
    }
}

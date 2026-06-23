using AIResumeAnalyzer.Application.Features.JobMatching.Commands.AnalyzeJobMatch;
using AIResumeAnalyzer.Application.Features.JobMatching.Commands.Queries.GetJobMatchById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIResumeAnalyzer.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/job-matches")]
    public class JobMatchesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobMatchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{jobMatchId}")]
        public async Task<IActionResult> GetById(Guid jobMatchId)
        {
            var result = await _mediator.Send(new GetJobMatchByIdQuery(jobMatchId));

            return Ok(result);
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze(AnalyzeJobMatchCommand command)
        {
            var id = await _mediator.Send(command);

            return Ok(new
            {
                JobMatchId = id
            });
        }
    }
}

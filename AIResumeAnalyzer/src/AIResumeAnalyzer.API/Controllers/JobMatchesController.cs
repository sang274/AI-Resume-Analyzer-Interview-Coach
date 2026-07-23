using AIResumeAnalyzer.Application.Features.JobMatching.Commands.AnalyzeJobMatch;
using AIResumeAnalyzer.Application.Features.JobMatching.Commands.Queries.GetJobMatchById;
using AIResumeAnalyzer.Application.Features.JobMatching.Commands.Queries.GetJobMatchHistory;
using AIResumeAnalyzer.Application.Features.JobMatching.DTO;
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

        [HttpGet]
        public async Task<IActionResult> GetHistory([FromQuery] JobMatchFilterParams filter)
        {
            var result = await _mediator.Send(new GetJobMatchHistoryQuery(filter));

            return Ok(result);
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

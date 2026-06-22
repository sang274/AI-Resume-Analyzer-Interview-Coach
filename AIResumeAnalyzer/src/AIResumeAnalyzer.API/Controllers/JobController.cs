using AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Create;
using AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Queries.GetJobDescriptionById;
using AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Queries.GetListJobDescriptions;
using AIResumeAnalyzer.Application.Features.JobDescriptions.Commands.Queries.GetListMyJobDescriptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIResumeAnalyzer.API.Controllers
{
    [ApiController]
    [Route("api/Jobs")]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobDescriptions()
        {
            var result = await _mediator.Send(new GetListJobDescriptionsQuery());

            return Ok(result);
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyJobDescriptions()
        {
            var result = await _mediator.Send(new GetListMyJobDescriptionsQuery());

            return Ok(result);
        }

        [HttpGet("{jobDescriptionId}")]
        public async Task<IActionResult> GetById(Guid jobDescriptionId)
        {
            return Ok(await _mediator.Send(new GetJobDescriptionByIdQuery(jobDescriptionId)));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateJobDescriptionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}

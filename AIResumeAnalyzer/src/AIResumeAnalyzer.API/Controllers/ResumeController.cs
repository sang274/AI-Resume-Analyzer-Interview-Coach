using AIResumeAnalyzer.Application.Features.Resumes.Commands.Queries.GetListMyResumes;
using AIResumeAnalyzer.Application.Features.Resumes.Commands.UploadResume;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIResumeAnalyzer.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/resumes")]
    public class ResumeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResumeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var resumeId = await _mediator.Send(new UploadResumeCommand(file));

            return Ok(new
            {
                ResumeId = resumeId
            });
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyResumes()
        {
            var result =
                await _mediator.Send(
                    new GetListMyResumesQuery());

            return Ok(result);
        }
    }
}

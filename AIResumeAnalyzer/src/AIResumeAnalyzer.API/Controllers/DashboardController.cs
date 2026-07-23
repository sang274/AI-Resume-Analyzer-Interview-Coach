using AIResumeAnalyzer.Application.Features.Dashboard.Commands.Queries.GetDashboard;
using AIResumeAnalyzer.Application.Features.Dashboard.Commands.Queries.GetRecentActivities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIResumeAnalyzer.API.Controllers;

[Authorize]
[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetDashboardQuery());

        return Ok(result);
    }

    [HttpGet("activities")]
    public async Task<IActionResult> Activities(
    [FromQuery] int take = 10)
    {
        return Ok(await _mediator.Send(new GetRecentActivitiesQuery(take)));
    }
}
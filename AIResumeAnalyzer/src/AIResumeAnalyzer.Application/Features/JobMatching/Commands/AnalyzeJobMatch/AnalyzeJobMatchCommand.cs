using MediatR;

namespace AIResumeAnalyzer.Application.Features.JobMatching.Commands.AnalyzeJobMatch
{
    public record AnalyzeJobMatchCommand(Guid ResumeId, Guid JobDescriptionId) : IRequest<Guid>;
}

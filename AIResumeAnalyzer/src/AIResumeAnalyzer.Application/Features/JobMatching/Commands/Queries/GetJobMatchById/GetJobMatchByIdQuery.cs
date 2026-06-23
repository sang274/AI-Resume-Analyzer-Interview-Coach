using AIResumeAnalyzer.Application.Features.JobMatching.DTO;
using MediatR;

namespace AIResumeAnalyzer.Application.Features.JobMatching.Commands.Queries.GetJobMatchById
{
    public record GetJobMatchByIdQuery(Guid JobMatchId) : IRequest<JobMatchDetailResponse>;
}

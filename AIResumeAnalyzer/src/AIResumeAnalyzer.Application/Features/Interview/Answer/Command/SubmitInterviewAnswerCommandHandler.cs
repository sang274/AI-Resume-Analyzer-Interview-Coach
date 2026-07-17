using AIResumeAnalyzer.Application.Common.Exceptions;
using AIResumeAnalyzer.Application.Features.Interview.Answer.DTO;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IInterviewService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Interview.Answer.Command
{
    public class SubmitInterviewAnswerCommandHandler : IRequestHandler<SubmitInterviewAnswerCommand, SubmitInterviewAnswerResponse>
    {
        private readonly IInterviewQuestionRepository _questionRepository;

        private readonly IInterviewCoachService _interviewCoachService;

        private readonly ICurrentUserService _currentUserService;

        private readonly IUnitOfWork _unitOfWork;

        public SubmitInterviewAnswerCommandHandler(
            IInterviewQuestionRepository questionRepository,
            IInterviewCoachService interviewCoachService,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _questionRepository = questionRepository;
            _interviewCoachService = interviewCoachService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<SubmitInterviewAnswerResponse> Handle(SubmitInterviewAnswerCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            var question = await _questionRepository.GetDetailAsync(request.QuestionId, userId, cancellationToken);

            if (question is null)
            {
                throw new NotFoundException( "Interview question not found.");
            }

            question.Answer = request.Answer;

            var evaluation = await _interviewCoachService.EvaluateAnswerAsync(
                        question.Question,
                        request.Answer,
                        cancellationToken);

            question.Score = evaluation.Score;

            question.AIResponse = evaluation.Feedback;

            _questionRepository.Update(question);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new SubmitInterviewAnswerResponse
            {
                QuestionId = question.Id,
                Score = evaluation.Score,
                Feedback = evaluation.Feedback
            };
        }
    }
}

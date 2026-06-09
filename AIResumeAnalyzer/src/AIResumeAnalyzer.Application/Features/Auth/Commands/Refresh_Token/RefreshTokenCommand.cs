using AIResumeAnalyzer.Application.Features.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Features.Auth.Commands.Refresh_Token
{
    public record RefreshTokenCommand(
        string RefreshToken
    ) : IRequest<TokenResponse>;
}

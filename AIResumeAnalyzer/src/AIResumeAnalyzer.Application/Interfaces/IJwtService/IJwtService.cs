using AIResumeAnalyzer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Interfaces.IJwtService
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
    }
}

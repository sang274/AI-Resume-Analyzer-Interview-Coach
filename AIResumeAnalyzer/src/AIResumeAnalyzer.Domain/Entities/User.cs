using AIResumeAnalyzer.Domain.Common;
using AIResumeAnalyzer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.User;

        public ICollection<Resume> Resumes { get; set; }
            = new List<Resume>();

        public ICollection<InterviewSession> InterviewSessions { get; set; }
            = new List<InterviewSession>();

        public ICollection<RefreshToken> RefreshTokens { get; set; }
            = new List<RefreshToken>();
    }
}

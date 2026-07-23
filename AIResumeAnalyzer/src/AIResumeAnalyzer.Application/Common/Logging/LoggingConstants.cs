using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Common.Logging
{
    public static class LoggingConstants
    {
        public static readonly HashSet<string> SensitiveFields =
        [
            "Password",
            "PasswordHash",
            "NewPassword",
            "ConfirmPassword",
            "Token",
            "AccessToken",
            "RefreshToken",
            "Jwt",
            "JwtToken",
            "Secret",
            "SecretKey",
            "ApiKey",
            "ClientSecret"
        ];

        public const long SlowRequestThreshold = 2000;
    }
}

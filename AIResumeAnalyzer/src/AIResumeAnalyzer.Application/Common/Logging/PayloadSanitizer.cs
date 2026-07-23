using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Common.Logging
{
    public static class PayloadSanitizer
    {
        public static Dictionary<string, object?> Sanitize(object request)
        {
            var result = new Dictionary<string, object?>();

            foreach (var property in request.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var name = property.Name;

                if (LoggingConstants.SensitiveFields.Contains(name))
                {
                    result[name] = "***";
                    continue;
                }

                var value = property.GetValue(request);

                if (value is IFormFile file)
                {
                    result[name] = new
                    {
                        file.FileName,
                        file.Length,
                        file.ContentType
                    };

                    continue;
                }

                result[name] = value;
            }

            return result;
        }
    }
}

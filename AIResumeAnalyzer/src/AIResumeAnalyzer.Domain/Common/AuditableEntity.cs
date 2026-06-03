using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}

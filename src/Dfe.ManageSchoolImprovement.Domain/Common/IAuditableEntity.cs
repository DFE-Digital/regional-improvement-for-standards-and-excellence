using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageSchoolImprovement.Domain.Common
{
    public interface IAuditableEntity
   {
        DateTime CreatedOn { get; set; }
        string CreatedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }
        string? LastModifiedBy { get; set; }
    }
}

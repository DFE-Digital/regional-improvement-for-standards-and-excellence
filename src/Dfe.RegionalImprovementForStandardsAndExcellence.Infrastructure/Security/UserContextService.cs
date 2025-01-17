using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Infrastructure.Security
{
    public interface IUserContextService
    {
        string GetCurrentUsername();
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System"; // Default to "System" if null
        }
    }

}

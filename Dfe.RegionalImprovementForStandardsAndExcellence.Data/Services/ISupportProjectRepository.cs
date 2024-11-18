using Dfe.RegionalImprovementForStandardsAndExcellence.Data.Models;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Data.Services;

public interface ISupportProjectRepository
{
    Task<ApiResponse<IEnumerable<SupportProject>>> GetAllSupportProjects();
}
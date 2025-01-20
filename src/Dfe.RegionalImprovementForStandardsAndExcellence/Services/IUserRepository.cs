using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
}
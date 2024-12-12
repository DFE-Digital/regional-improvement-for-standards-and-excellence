using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
}
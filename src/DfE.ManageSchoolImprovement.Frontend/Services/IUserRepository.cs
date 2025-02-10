using DfE.ManageSchoolImprovement.Frontend.Models;

namespace DfE.ManageSchoolImprovement.Frontend.Services;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
}

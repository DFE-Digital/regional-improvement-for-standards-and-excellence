using Dfe.Academisation.ExtensionMethods;
using DfE.ManageSchoolImprovement.Frontend.Services.AzureAd;
using User = DfE.ManageSchoolImprovement.Frontend.Models.User;

namespace DfE.ManageSchoolImprovement.Frontend.Services;

public class UserRepository(IGraphUserService graphUserService) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAllUsers()
   {
      IEnumerable<Microsoft.Graph.User> users = await graphUserService.GetAllUsers();

      return users
         .Select(u => new User(u.Id, u.Mail, $"{u.GivenName} {u.Surname.ToFirstUpper()}"));
   }
}

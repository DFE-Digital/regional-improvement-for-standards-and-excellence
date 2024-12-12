using Dfe.Academisation.ExtensionMethods;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services.AzureAd;
using User = Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models.User;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services;

public class UserRepository : IUserRepository
{
   private readonly IGraphUserService _graphUserService;

   public UserRepository(IGraphUserService graphUserService)
   {
      _graphUserService = graphUserService;
   }

   public async Task<IEnumerable<User>> GetAllUsers()
   {
      IEnumerable<Microsoft.Graph.User> users = await _graphUserService.GetAllUsers();

      return users
         .Select(u => new User(u.Id, u.Mail, $"{u.GivenName} {u.Surname.ToFirstUpper()}"));
   }
}

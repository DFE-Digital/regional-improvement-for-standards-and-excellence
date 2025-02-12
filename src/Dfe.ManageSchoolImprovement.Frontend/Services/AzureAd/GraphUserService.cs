using Dfe.ManageSchoolImprovement.Frontend.Models;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using User = Microsoft.Graph.User;

namespace Dfe.ManageSchoolImprovement.Frontend.Services.AzureAd;

public class GraphUserService(IGraphClientFactory graphClientFactory, IOptions<AzureAdOptions> azureAdOptions) : IGraphUserService
{
    private readonly AzureAdOptions _azureAdOptions = azureAdOptions.Value;
    private readonly GraphServiceClient _client = graphClientFactory.Create();

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        List<User> users = new();

        List<QueryOption> queryOptions = new() { new("$count", "true"), new("$top", "999") };

        IGroupMembersCollectionWithReferencesPage members = await _client.Groups[_azureAdOptions.GroupId.ToString()].Members
            .Request(queryOptions)
            .Header("ConsistencyLevel", "eventual")
            .Select("givenName,surname,id,mail")
            .GetAsync();

        users.AddRange(members.Cast<User>().ToList());

        return users;
    }
}

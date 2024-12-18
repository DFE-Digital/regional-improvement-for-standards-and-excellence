using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using User = Microsoft.Graph.User;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services.AzureAd;

public class GraphUserService : IGraphUserService
{
    private readonly AzureAdOptions _azureAdOptions;
    private readonly GraphServiceClient _client;
    
    public GraphUserService(IGraphClientFactory graphClientFactory, IOptions<AzureAdOptions> azureAdOptions)
    {
        _client = graphClientFactory.Create();
        _azureAdOptions = azureAdOptions.Value;
    }

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
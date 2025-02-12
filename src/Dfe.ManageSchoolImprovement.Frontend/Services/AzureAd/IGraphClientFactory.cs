using Microsoft.Graph;

namespace Dfe.ManageSchoolImprovement.Frontend.Services.AzureAd;

public interface IGraphClientFactory
{
   public GraphServiceClient Create();
}

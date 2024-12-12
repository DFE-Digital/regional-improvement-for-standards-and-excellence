using Microsoft.Graph;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services.AzureAd;

public interface IGraphClientFactory
{
   public GraphServiceClient Create();
}

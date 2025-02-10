using Microsoft.Graph;

namespace DfE.ManageSchoolImprovement.Frontend.Services.AzureAd;

public interface IGraphClientFactory
{
   public GraphServiceClient Create();
}

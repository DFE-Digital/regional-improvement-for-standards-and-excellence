namespace Dfe.RegionalImprovementForStandardsAndExcellence.Api.Client.Security
{
    public interface ITokenAcquisitionService
    {
        Task<string> GetTokenAsync();
    }
}

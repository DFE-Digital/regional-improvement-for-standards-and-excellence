using System.Net;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services.Http;

public class ApiResponse<TBody>(HttpStatusCode statusCode, TBody body)
{
    public bool Success { get; } = (int)statusCode >= 200 && (int)statusCode < 300;
    public HttpStatusCode StatusCode { get; } = statusCode;
    public TBody Body { get; } = body;
}

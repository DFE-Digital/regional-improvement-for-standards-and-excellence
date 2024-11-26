using System;
using System.Runtime.Serialization;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Services.Http;

[Serializable]
public class ApiResponseException : Exception
{
    public ApiResponseException(string message) : base(message)
    {
    }

    protected ApiResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

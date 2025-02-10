using System.Runtime.Serialization;

namespace DfE.ManageSchoolImprovement.Frontend.Services.Http;

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

using Dfe.ManageSchoolImprovement.Frontend.Security;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;
using System.Reflection;

namespace Dfe.ManageSchoolImprovement.Frontend.Tests.Security
{
    public class SecurityHeadersDefinitionsTests
    {
        [Theory]
        [InlineData("X-Frame-Options", "DENY")]
        [InlineData("X-Content-Type-Options", "nosniff")]
        [InlineData("Referrer-Policy", "strict-origin-when-cross-origin")]
        [InlineData("Cross-Origin-Opener-Policy", "same-origin")]
        [InlineData("Cross-Origin-Embedder-Policy", "require-corp")]
        [InlineData("Cross-Origin-Resource-Policy", "same-origin")]
        [InlineData("X-XSS-Protection", "0")]
        [InlineData("Server", "")] 
        public void GetHeaderPolicyCollection_ShouldContainHeaderWithCorrectValues(string headerKey, string headerValue)
        {
            // Arrange
            var policy = SecurityHeadersDefinitions.GetHeaderPolicyCollection(isDev: false);

            // Act & Assert
            Assert.Contains(policy, header => header.Key == headerKey && GetHeaderValue(policy, headerKey) == headerValue);
        }

        [Theory]
        [InlineData("Content-Security-Policy")]
        [InlineData("Permissions-Policy")]
        public void GetHeaderPolicyCollection_ShouldContainHeader(string headerKey)
        {
            // Arrange
            var policy = SecurityHeadersDefinitions.GetHeaderPolicyCollection(isDev: false); 

            // Act & Assert
            Assert.Contains(policy, header => header.Key == headerKey);
        } 

        public static string GetHeaderValue(Dictionary<string, IHeaderPolicy> policy, string headerKey)
        {
            if (!policy.TryGetValue(headerKey, out var header))
            {
                throw new Exception($"{headerKey} header not found.");
            }

            // Use reflection to access the private '_value' field
            var headerValueField = header.GetType().GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance); 
            if (headerValueField == null)
            {
                throw new Exception($"_value field not found in {headerKey} header.");
            }

            // Extract and return the private field value
            return headerValueField.GetValue(header)?.ToString() ?? throw new Exception($"{headerKey} value is null.");
        } 
    }
}

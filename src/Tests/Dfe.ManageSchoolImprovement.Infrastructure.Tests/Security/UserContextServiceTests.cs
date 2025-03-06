using Dfe.ManageSchoolImprovement.Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Moq; 
using System.Security.Claims; 

namespace Dfe.ManageSchoolImprovement.Infrastructure.Tests.Security
{
    public class UserContextServiceTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly UserContextService _userContextService;

        public UserContextServiceTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userContextService = new UserContextService(_httpContextAccessorMock.Object);
        }

        [Fact]
        public void GetCurrentUsername_ShouldReturnUsername_WhenUserIsAuthenticated()
        {
            // Arrange
            var username = "testuser";
            var claims = new List<Claim> { new(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = claimsPrincipal };

            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            // Act
            var result = _userContextService.GetCurrentUsername();

            // Assert
            Assert.Equal(username, result);
        }

        [Fact]
        public void GetCurrentUsername_ShouldReturnSystem_WhenUserIsNotAuthenticated()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();

            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            // Act
            var result = _userContextService.GetCurrentUsername();

            // Assert
            Assert.Equal("System", result);
        }

        [Fact]
        public void GetCurrentUsername_ShouldReturnSystem_WhenHttpContextIsNull()
        {
            // Arrange
            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns((HttpContext)null!);

            // Act
            var result = _userContextService.GetCurrentUsername();

            // Assert
            Assert.Equal("System", result);
        }
    }
}

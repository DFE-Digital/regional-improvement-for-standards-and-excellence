using DfE.ManageSchoolImprovement.Frontend.Services.AzureAd;
using DfE.ManageSchoolImprovement.Frontend.Services;
using Moq;

namespace DfE.ManageSchoolImprovement.Frontend.Tests.Services
{
    public class UserRepositoryTests
    {
        private readonly Mock<IGraphUserService> _mockGraphUserService;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _mockGraphUserService = new Mock<IGraphUserService>();
            _userRepository = new UserRepository(_mockGraphUserService.Object);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsMappedUsers()
        {
            // Arrange
            var graphUsers = new List<Microsoft.Graph.User>
            {
                new() { Id = "1", Mail = "John.Doe@education.gov.uk", GivenName = "John", Surname = "Doe" },
                new() { Id = "2", Mail = "Jane.Smith@education.gov.uk", GivenName = "Jane", Surname = "Smith" }
            };

            _mockGraphUserService.Setup(service => service.GetAllUsers()).ReturnsAsync(graphUsers);

            // Act
            var users = await _userRepository.GetAllUsers();

            // Assert
            Assert.Equal(2, users.Count());
            Assert.Equal("1", users.First().Id);
            Assert.Equal("John.Doe@education.gov.uk", users.First().EmailAddress);
            Assert.Equal("John Doe", users.First().FullName);
            Assert.Equal("2", users.Last().Id);
            Assert.Equal("Jane.Smith@education.gov.uk", users.Last().EmailAddress);
            Assert.Equal("Jane Smith", users.Last().FullName);
        }
    }
}

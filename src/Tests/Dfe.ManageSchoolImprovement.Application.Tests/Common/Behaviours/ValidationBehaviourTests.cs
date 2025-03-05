using Dfe.ManageSchoolImprovement.Application.Common.Behaviours;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;

namespace Dfe.ManageSchoolImprovement.Application.Tests.Common.Behaviours
{
    public class ValidationBehaviourTests
    {
        private readonly Mock<RequestHandlerDelegate<TestResponse>> _mockNext;

        public ValidationBehaviourTests() => _mockNext = new Mock<RequestHandlerDelegate<TestResponse>>();

        [Fact]
        public async Task Handle_ReturnsResponse_WhenNoValidatorsPresent()
        {
            // Arrange
            var response = new TestResponse();
            _mockNext.Setup(n => n()).ReturnsAsync(response);

            var behaviour = new ValidationBehaviour<TestRequest, TestResponse>([]);

            // Act
            var result = await behaviour.Handle(new TestRequest(), _mockNext.Object, CancellationToken.None);

            // Assert
            Assert.Equal(response, result);
            _mockNext.Verify(n => n(), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsResponse_WhenValidationPasses()
        {
            // Arrange
            var response = new TestResponse();
            _mockNext.Setup(n => n()).ReturnsAsync(response);

            var validator = new Mock<IValidator<TestRequest>>();
            validator.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new ValidationResult()); // No validation errors

            var validators = new List<IValidator<TestRequest>> { validator.Object };
            var behaviour = new ValidationBehaviour<TestRequest, TestResponse>(validators);

            // Act
            var result = await behaviour.Handle(new TestRequest(), _mockNext.Object, CancellationToken.None);

            // Assert
            Assert.Equal(response, result);
            _mockNext.Verify(n => n(), Times.Once);
            validator.Verify(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ThrowsValidationException_WhenValidationFails()
        {
            // Arrange
            var validationFailure = new ValidationFailure("Property", "Validation error");
            var validationResult = new ValidationResult(new List<ValidationFailure> { validationFailure });

            var validator = new Mock<IValidator<TestRequest>>();
            validator.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(validationResult);

            var validators = new List<IValidator<TestRequest>> { validator.Object };
            var behaviour = new ValidationBehaviour<TestRequest, TestResponse>(validators);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Application.Common.Exceptions.ValidationException>(() =>
                behaviour.Handle(new TestRequest(), _mockNext.Object, CancellationToken.None));

            Assert.Single(exception.Errors);
            Assert.Equal("Validation error", exception.Errors.First().Value.FirstOrDefault());
            validator.Verify(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockNext.Verify(n => n(), Times.Never);
        }
    }
}

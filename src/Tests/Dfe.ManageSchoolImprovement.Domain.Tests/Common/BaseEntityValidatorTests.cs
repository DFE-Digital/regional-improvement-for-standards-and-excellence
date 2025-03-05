using Dfe.ManageSchoolImprovement.Domain.Common;
using FluentValidation; 

namespace Dfe.ManageSchoolImprovement.Domain.Tests.Common
{
    public class BaseEntityValidatorTests
    {
        private class TestEntity
        {
            public string Name { get; set; } = null!;
        }

        private class TestValidator : BaseEntityValidator<TestEntity>
        {
            protected override IEnumerable<IValidator<TestEntity>> GetValidationRules()
            {
                yield return new InlineValidator<TestEntity>
                {
                    v => v.RuleFor(x => x.Name).NotEmpty()
                };
            }
        }

        [Fact]
        public void ValidateAndThrow_ShouldNotThrow_WhenValidationSucceeds()
        {
            // Arrange
            var validator = new TestValidator();
            var entity = new TestEntity { Name = "Valid Name" };

            // Act & Assert
            validator.ValidateAndThrow(entity);
        }

        [Fact]
        public void ValidateAndThrow_ShouldThrowValidationException_WhenValidationFails()
        {
            // Arrange
            var validator = new TestValidator();
            var entity = new TestEntity { Name = string.Empty };

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(entity));
            Assert.Contains(exception.Errors, e => e.PropertyName == nameof(TestEntity.Name));
        }

        [Fact]
        public void Constructor_ShouldIncludeAllValidationRules()
        {
            // Arrange
            var validator = new TestValidator();
            var entity = new TestEntity { Name = string.Empty };

            // Act
            var validationResult = validator.Validate(entity);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == nameof(TestEntity.Name));
        }
    }
}

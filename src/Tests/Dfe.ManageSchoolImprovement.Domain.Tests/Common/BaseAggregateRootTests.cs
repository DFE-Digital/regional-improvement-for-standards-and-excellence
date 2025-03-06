using Dfe.ManageSchoolImprovement.Domain.Common;
using Moq;

namespace Dfe.ManageSchoolImprovement.Domain.Tests.Common
{ 
    public class BaseAggregateRootTests
    {
        private readonly Mock<IDomainEvent> _mockDomainEvent;
        public BaseAggregateRootTests()
        {
            _mockDomainEvent = new Mock<IDomainEvent>();
        }
        private class TestAggregateRoot : BaseAggregateRoot
        {
            public new void AddDomainEvent(IDomainEvent domainEvent)
            {
                base.AddDomainEvent(domainEvent);
            }

            public new void RemoveDomainEvent(IDomainEvent domainEvent)
            {
                base.RemoveDomainEvent(domainEvent);
            }

            public new void ClearDomainEvents()
            {
                base.ClearDomainEvents();
            }
        } 

        [Fact]
        public void DomainEvents_ShouldBeEmpty_WhenInitialized()
        {
            // Arrange
            var aggregateRoot = new TestAggregateRoot();

            // Act
            var domainEvents = aggregateRoot.DomainEvents;

            // Assert
            Assert.Empty(domainEvents);
        }

        [Fact]
        public void AddDomainEvent_ShouldAddEventToDomainEvents()
        {
            // Arrange
            var aggregateRoot = new TestAggregateRoot();

            // Act
            aggregateRoot.AddDomainEvent(_mockDomainEvent.Object);

            // Assert
            Assert.Contains(_mockDomainEvent.Object, aggregateRoot.DomainEvents);
        }

        [Fact]
        public void RemoveDomainEvent_ShouldRemoveEventFromDomainEvents()
        {
            // Arrange
            var aggregateRoot = new TestAggregateRoot(); 
            aggregateRoot.AddDomainEvent(_mockDomainEvent.Object);

            // Act
            aggregateRoot.RemoveDomainEvent(_mockDomainEvent.Object);

            // Assert
            Assert.DoesNotContain(_mockDomainEvent.Object, aggregateRoot.DomainEvents);
        }

        [Fact]
        public void ClearDomainEvents_ShouldRemoveAllEventsFromDomainEvents()
        {
            // Arrange
            var aggregateRoot = new TestAggregateRoot(); 
            aggregateRoot.AddDomainEvent(_mockDomainEvent.Object);
            aggregateRoot.AddDomainEvent(_mockDomainEvent.Object);

            // Act
            aggregateRoot.ClearDomainEvents();

            // Assert
            Assert.Empty(aggregateRoot.DomainEvents);
        }
    }
}

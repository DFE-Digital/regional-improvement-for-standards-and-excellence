using Dfe.RegionalImprovementForStandardsAndExcellence.Application.Common.EventHandlers;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.Schools.EventHandlers
{
    public class SchoolCreatedEventHandler(ILogger<SchoolCreatedEventHandler> logger)
        : BaseEventHandler<SchoolCreatedEvent>(logger)
    {
        protected override Task HandleEvent(SchoolCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Test logic for SchoolCreatedEvent executed.");
            return Task.CompletedTask;
        }
    }
}

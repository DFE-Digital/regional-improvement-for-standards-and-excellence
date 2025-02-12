//using Dfe.ManageSchoolImprovement.Application.Common.EventHandlers;
//using Dfe.ManageSchoolImprovement.Domain.Events;
//using Microsoft.Extensions.Logging;

//namespace Dfe.ManageSchoolImprovement.Application.Schools.EventHandlers
//{
//    public class SchoolCreatedEventHandler(ILogger<SchoolCreatedEventHandler> logger)
//        : BaseEventHandler<SchoolCreatedEvent>(logger)
//    {
//        protected override Task HandleEvent(SchoolCreatedEvent notification, CancellationToken cancellationToken)
//        {
//            logger.LogInformation("Test logic for SchoolCreatedEvent executed.");
//            return Task.CompletedTask;
//        }
//    }
//}

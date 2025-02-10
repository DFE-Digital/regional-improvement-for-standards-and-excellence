//using DfE.ManageSchoolImprovement.Application.Common.EventHandlers;
//using DfE.ManageSchoolImprovement.Domain.Events;
//using Microsoft.Extensions.Logging;

//namespace DfE.ManageSchoolImprovement.Application.Schools.EventHandlers
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

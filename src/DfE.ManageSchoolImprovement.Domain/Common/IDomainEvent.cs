using MediatR;

namespace DfE.ManageSchoolImprovement.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}

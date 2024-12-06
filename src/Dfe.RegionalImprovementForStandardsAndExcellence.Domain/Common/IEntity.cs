namespace Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Common
{
    public interface IEntity<out TId> where TId : IStronglyTypedId
    {
        TId? Id { get; }

        DateTime CreatedOn { get; }
        string CreatedBy { get; }
        DateTime? LastModifiedOn { get; }
        string? LastModifiedBy { get; } 
    }
}

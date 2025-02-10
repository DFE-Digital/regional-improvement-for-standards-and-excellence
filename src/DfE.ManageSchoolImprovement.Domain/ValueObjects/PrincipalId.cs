using DfE.ManageSchoolImprovement.Domain.Common;

namespace DfE.ManageSchoolImprovement.Domain.ValueObjects
{
    public record PrincipalId(int Value) : IStronglyTypedId;
}

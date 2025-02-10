using DfE.ManageSchoolImprovement.Domain.Common;

namespace DfE.ManageSchoolImprovement.Domain.ValueObjects
{
    public record SchoolId(int Value) : IStronglyTypedId;

}

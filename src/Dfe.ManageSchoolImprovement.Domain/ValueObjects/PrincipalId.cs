using Dfe.ManageSchoolImprovement.Domain.Common;

namespace Dfe.ManageSchoolImprovement.Domain.ValueObjects
{
    public record PrincipalId(int Value) : IStronglyTypedId;
}

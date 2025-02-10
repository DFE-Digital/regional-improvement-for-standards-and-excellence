using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace DfE.ManageSchoolImprovement.Tests.Common.Customizations.Attributes
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
           : base(() => new Fixture().Customize(new CompositeCustomization(new AutoMoqCustomization())))
        {
        }
    }
}

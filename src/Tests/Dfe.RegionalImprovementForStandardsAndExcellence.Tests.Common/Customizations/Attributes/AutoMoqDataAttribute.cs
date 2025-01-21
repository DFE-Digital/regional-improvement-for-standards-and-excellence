using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Tests.Common.Customizations.Attributes
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
           : base(() => new Fixture().Customize(new CompositeCustomization(new AutoMoqCustomization())))
        {
        }
    }
}

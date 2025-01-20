using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.ViewModels;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.TagHelpers;

[HtmlTargetElement("govuk-checkbox-input", TagStructure = TagStructure.WithoutEndTag)]
public class CheckboxInputTagHelper(IHtmlHelper htmlHelper) : InputTagHelperBase(htmlHelper)
{
    protected override async Task<IHtmlContent> RenderContentAsync()
   {
      CheckboxInputViewModel model = new() { Heading = Heading, Id = Id, Name = Name, Label = Label, Value = For.Model?.ToString(), HeadingStyle = HeadingStyle };

      return await _htmlHelper.PartialAsync("_CheckboxInput", model);
   }
}

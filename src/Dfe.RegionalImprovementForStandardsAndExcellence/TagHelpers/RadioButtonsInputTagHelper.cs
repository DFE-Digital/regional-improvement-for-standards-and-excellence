using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.ViewModels;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.TagHelpers;

[HtmlTargetElement("govuk-radiobuttons-input", TagStructure = TagStructure.WithoutEndTag)]
public class RadioButtonsInputTagHelper(IHtmlHelper htmlHelper) : InputTagHelperBase(htmlHelper)
{
    public IList<RadioButtonsLabelViewModel> RadioButtons { get; set; } = [];
    protected override async Task<IHtmlContent> RenderContentAsync()
    {
        RadioButtonViewModel model = new() { Name = Name, Hint = Hint, Value = For.Model?.ToString(), RadioButtons = RadioButtons };

        return await _htmlHelper.PartialAsync("_RadioButtons", model);
    }
}

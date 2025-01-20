using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.TagHelpers;

public class SubMenuLinkTagHelper(IHtmlGenerator generator) : AnchorTagHelper(generator)
{
   private const string PAGE = "page";

    public override void Process(TagHelperContext context, TagHelperOutput output)
   {
      string page = ViewContext.RouteData.Values[PAGE]!.ToString();
      if (page == Page)
      {
         output.Attributes.SetAttribute("aria-current", PAGE);
      }

      output.TagName = "a";

      base.Process(context, output);
   }
}

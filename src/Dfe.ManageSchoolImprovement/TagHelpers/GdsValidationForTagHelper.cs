using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;


namespace Dfe.ManageSchoolImprovement.Frontend.TagHelpers;

[HtmlTargetElement("p", Attributes = GdsValidationForAttributeName)]
public class GdsValidationForTagHelper : TagHelper
{
   private const string GdsValidationForAttributeName = "asp-gds-validation-for";

   [HtmlAttributeNotBound]
   [ViewContext]
   public ViewContext ViewContext { get; set; }

   [HtmlAttributeName(GdsValidationForAttributeName)]
   public ModelExpression For { get; set; }

   public override void Process(TagHelperContext context, TagHelperOutput output)
   {
      base.Process(context, output);
      if (For == null)
      {
         output.SuppressOutput();
         return;
      }

      ViewContext.ViewData.ModelState.TryGetValue(For.Name, out ModelStateEntry modelStateEntry);
      if (modelStateEntry != null && modelStateEntry.Errors.Count > 0)
      {
         TagBuilder builder = new("p");
         builder.Attributes.Add("id", For.Name + "-error");
         builder.AddCssClass("govuk-error-message");
         output.MergeAttributes(builder);
         output.PreContent.SetHtmlContent("<span class='govuk-visually-hidden'>Error:</span>");
         output.Content.SetHtmlContent(modelStateEntry.Errors.FirstOrDefault()?.ErrorMessage);
      }
      else
      {
         output.SuppressOutput();
      }
   }
}

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dfe.ManageSchoolImprovement.Frontend.Pages.TaskList.AllocateAdviser;

public class RiseAdviserEmailAttribute : ValidationAttribute
{
    private const string EmailPattern = @"^rise\.[a-zA-Z]+(?:-[a-zA-Z]+)?\.[a-zA-Z]+(?:-[a-zA-Z]+)?@education\.gov\.uk$";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string email && Regex.IsMatch(email, EmailPattern, RegexOptions.IgnoreCase))
        {
            return ValidationResult.Success!;
        }

        return new ValidationResult("Email must be in the format: rise.firstname.lastname@education.gov.uk");
    }
}

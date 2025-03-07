using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dfe.ManageSchoolImprovement.Frontend.Pages.TaskList.AddSupportingOrganisationContactDetails;

public class EmailValidationAttribute : ValidationAttribute
{
    private static readonly Regex EmailFormatRegex = new(@"^[^@\s]+@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*\.[A-Za-z]{2,}$", RegexOptions.Compiled);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string email)
        {
            if (!EmailFormatRegex.IsMatch(email))
            {
                return new ValidationResult("Email address must be in the correct format.");
            }
        }

        return ValidationResult.Success;
    }
}

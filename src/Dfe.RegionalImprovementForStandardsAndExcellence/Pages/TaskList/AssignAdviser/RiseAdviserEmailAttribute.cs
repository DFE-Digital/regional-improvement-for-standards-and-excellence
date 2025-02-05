namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Pages.TaskList.AssignAdviser
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class RiseAdviserEmailAttribute : ValidationAttribute
    {
        private const string EmailPattern = @"^rise\.[a-zA-Z]+(?:-[a-zA-Z]+)?\.[a-zA-Z]+(?:-[a-zA-Z]+)?@education\.gov\.uk$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            const string errorMessage = "Email must be in the format: rise.firstname.lastname@education.gov.uk";
            
            if (value is string whitespace && whitespace.Any(char.IsWhiteSpace))
            {
                return new ValidationResult(errorMessage);
            }
            
            if (value is string email && Regex.IsMatch(email, EmailPattern, RegexOptions.IgnoreCase))
            {
                return ValidationResult.Success!;
            }
            
            return new ValidationResult(errorMessage);
        }
    }

}

using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Utils;

#nullable enable
public static class StringExtensions
{
    public static bool IsEmpty(this string input) => string.IsNullOrWhiteSpace(input);

    public static bool IsPresent(this string input) => !input.IsEmpty();

    public static string FullNameFromEmail(this string input) => NameFromEmail(input); 
    public static string NameFromEmail(string email)
    {
        if(!email.EndsWith("@EDUCATION.GOV.UK", StringComparison.OrdinalIgnoreCase))
        {
            return email;
        }
        
        string namePart = email.Substring(0, email.IndexOf("@EDUCATION.GOV.UK", StringComparison.Ordinal));
        string[] nameParts = namePart.Split('.');

        if (nameParts.Length == 2)
        {
            string firstName = nameParts[0];
            string lastName = nameParts[1];
            string captializedLastName = char.ToUpper(lastName[0]) + lastName.Substring(1).ToLower();

            return $"{firstName} {captializedLastName}";
        }

        return email;
    }
}
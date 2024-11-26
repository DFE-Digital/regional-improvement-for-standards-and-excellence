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
}
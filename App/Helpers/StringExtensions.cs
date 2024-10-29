using System.Globalization;

namespace GuestlineChallenge.Helpers;

public static class StringExtensions
{
    public static DateTime Parse(this string input, string format = "yyyyMMdd")
    {
        return DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);
    }
    
    public static bool TryParse(this string input, out DateTime result, string format = "yyyyMMdd")
    {
        return DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
    }
}
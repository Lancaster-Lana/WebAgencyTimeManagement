using System;

/// <summary>
/// Summary description for ExtensionMethods
/// </summary>
public static class ExtensionMethods
{
    public static bool IsWeekend(this DateTime d)
    {
        if ((d.DayOfWeek == DayOfWeek.Saturday) || (d.DayOfWeek == DayOfWeek.Sunday))
        {
            return true;
        }
        return false;
    }

    public static string ToStandardDateFormat(this DateTime d)
    {
        return d.ToString("MM/dd/yyyy");
    }
}

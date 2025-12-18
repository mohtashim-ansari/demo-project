using System;

namespace dsr_admin.Helpers;

public static class DateTimeStringHelper
{
    public static string DateToString(this DateTime date)
    {
        return date.ToString("yyyy-MM-dd");
    }

    public static string DateTimeToString(this DateTime date)
    {
        return date.ToString("yyyy-MM-dd hh:mm tt");
    }

    public static string DateToString(this DateTime? date)
    {
        return date.HasValue
            ? date.Value.ToString("yyyy-MM-dd")
            : string.Empty;
    }

    public static string DateTimeToString(this DateTime? date)
    {
        return date.HasValue
            ? date.Value.ToString("yyyy-MM-dd hh:mm tt")
            : string.Empty;
    }
}

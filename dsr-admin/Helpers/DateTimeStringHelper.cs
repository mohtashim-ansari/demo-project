using System;

namespace dsr_admin.Helpers;

public static class DateTimeStringHelper
{
    public static string DateToString(this DateTime date)
    {
        return date.ToString("yyyy-MM-dd hh:mm tt");
    }
}

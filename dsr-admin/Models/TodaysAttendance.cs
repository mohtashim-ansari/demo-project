using System;

namespace dsr_admin.Models;

public class TodaysAttendance
{
    public int Id { get; set; }
    public bool IsPresent { get; set; }
    public bool IsDSRSent { get; set; }
    public DateTime AttandanceDate { get; set; }
}

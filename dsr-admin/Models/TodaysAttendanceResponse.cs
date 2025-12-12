using System;

namespace dsr_admin.Models;

public class TodaysAttendanceResponse
{
    public int Id { get; set; }
    public string? FirtName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public DateTime AttandanceDate { get; set; }
    public bool IsPresent { get; set; }
}
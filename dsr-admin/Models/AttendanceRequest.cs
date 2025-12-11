using System;

namespace dsr_admin.Models;

public class AttendanceRequest : BaseModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime AttandanceDate { get; set; }
    public bool IsPresent { get; set; }
    public bool IsDSRSent { get; set; }
}

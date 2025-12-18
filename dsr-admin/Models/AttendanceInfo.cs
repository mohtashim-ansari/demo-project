using System;

namespace dsr_admin.Models;

public class AttendanceInfo 
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime AttandanceDate { get; set; }
    public bool IsPresent { get; set; }
    public bool IsDSRSent { get; set; }

    public bool IsDeleted { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
}

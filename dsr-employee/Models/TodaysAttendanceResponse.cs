namespace dsr_employee.Models;

public class TodaysAttendanceResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;

    public DateTime AttandanceDate { get; set; } 
    public bool IsPresent { get; set; }
    public bool IsDSRSent { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}

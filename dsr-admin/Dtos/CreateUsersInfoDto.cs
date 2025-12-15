using System.ComponentModel.DataAnnotations;

public class CreateUsersInfoDto
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirtName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required."), EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mobile number is required.")]
    [RegularExpression(@"^\d{10}$")]
    public string Mobile { get; set; } = string.Empty;

    [Required(ErrorMessage = "User name is required.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string PasswordHash { get; set; } = string.Empty;

    // API REQUIRED FIELDS
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
}

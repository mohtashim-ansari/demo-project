using System;
using System.ComponentModel.DataAnnotations;

namespace dsr_admin.Dtos;

public record CreateUsersInfoDto
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirtName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mobile number is required.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits.")]
    public string Mobile { get; set; } = string.Empty;

    [Required(ErrorMessage = "Role is required.")]
    public int? UserRoleId { get; set; }

    [Required(ErrorMessage = "User name is required.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters and contain uppercase, lowercase, number, and special character.")]
    public string PasswordHash { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm password is required.")]
    [Compare(nameof(PasswordHash), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    // API required fields
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
}

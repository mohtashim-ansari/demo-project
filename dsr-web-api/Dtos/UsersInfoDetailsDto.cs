using System.ComponentModel.DataAnnotations;

namespace dsr_web_api.Dtos;

public record class UsersInfoDetailsDto
(
    [Required] int Id,
    [Required] string? FirtName,
    [Required] string? LastName,
    [Required] string? Email,
    [Required] string? Mobile,
    [Required] string? UserName,
    [Required] string? PasswordHash,
    bool IsActive,
    DateTime LoginDateTime,
    DateTime LastLoginDateTime,
    [Required] bool IsDeleted,
    [Required] int CreatedBy,
    [Required] DateTime CreatedOn,
    int? UpdatedBy,
    DateTime? UpdatedOn
);

namespace dsr_admin.Dtos;

public record class UserDto
(
    int Id,
    string? FirtName,
    string? LastName,
    string? Email,
    string? Mobile,
    int UserRoleId,
    string? UserName,
    string? PasswordHash,
    bool IsActive,
    DateTime LoginDateTime,
    DateTime LastLoginDateTime,
    bool IsDeleted,
    int CreatedBy,
    DateTime CreatedOn,
    int? UpdatedBy,
    DateTime? UpdatedOn
);

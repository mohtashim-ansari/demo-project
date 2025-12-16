using System.ComponentModel.DataAnnotations;

namespace dsr_admin.Dtos;

public record class RoleDto
(
    [Required] int Id,
    [Required] string Name,
    [Required] string Description,
    bool IsDefault
);
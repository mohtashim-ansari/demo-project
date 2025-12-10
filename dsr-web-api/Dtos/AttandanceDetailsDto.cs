using System.ComponentModel.DataAnnotations;

namespace dsr_web_api.Dtos;

public record class AttandanceDetailsDto
(
    [Required] int Id,
    [Required] int UserId,
    [Required] DateTime AttandanceDate,
    bool IsPresent,
    bool IsDSRSent,
    [Required] bool IsDeleted,
    [Required] int CreatedBy,
    [Required] DateTime CreatedOn,
    int? UpdatedBy,
    DateTime? UpdatedOn
);

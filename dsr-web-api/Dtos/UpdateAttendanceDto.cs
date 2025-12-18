using System.ComponentModel.DataAnnotations;

namespace dsr_web_api.Dtos;


public record class UpdateAttendanceDto
(
    int Id,
    int UserId,
    DateTime AttendanceDate,
    bool IsPresent,
    bool IsDSRSent,
    bool IsDeleted,
    int CreatedBy,
    DateTime? CreatedOn,
    int? UpdatedBy,
    DateTime? UpdatedOn
);


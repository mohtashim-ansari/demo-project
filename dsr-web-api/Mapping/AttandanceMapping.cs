using System;
using dsr_web_api.Dtos;
using dsr_web_api.Models;

namespace dsr_web_api.Mapping;

public static class AttandanceInfoMapping
{
    // Convert CreateAttandanceDto → AttandanceInfo entity
    public static AttandanceInfo ToEntity(this CreateAttandanceDto dto)
    {
        return new AttandanceInfo()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            AttandanceDate = dto.AttandanceDate,
            IsPresent = dto.IsPresent,
            IsDSRSent = dto.IsDSRSent,
            IsDeleted = dto.IsDeleted,
            CreatedBy = dto.CreatedBy,
            CreatedOn = dto.CreatedOn
        };
    }

    // Convert UpdateAttandaceDto → AttandanceInfo entity
    public static AttandanceInfo ToEntity(this UpdateAttandaceDto dto, int id, AttandanceInfo existing)
    {
        return new AttandanceInfo()
        {
            Id = id,
            UserId = dto.UserId,
            AttandanceDate = dto.AttandanceDate,
            IsPresent = dto.IsPresent,
            IsDSRSent = dto.IsDSRSent,
            IsDeleted = dto.IsDeleted,
            CreatedBy = existing.CreatedBy,
            CreatedOn = existing.CreatedOn,
            UpdatedBy = dto.UpdatedBy,
            UpdatedOn = dto.UpdatedOn
        };
    }

    // Convert AttandanceInfo + UsersInfo → AttandanceDetailsDto
    public static AttandanceDetailsDto ToAttandanceDetailsDto(this AttandanceInfo attandance)
    {
        return new AttandanceDetailsDto
        (
            attandance.Id,
            attandance.UserId,
            attandance.AttandanceDate,
            attandance.IsPresent,
            attandance.IsDSRSent,
            attandance.IsDeleted,
            attandance.CreatedBy,
            attandance.CreatedOn,
            attandance.UpdatedBy,
            attandance.UpdatedOn
        );
    }
}

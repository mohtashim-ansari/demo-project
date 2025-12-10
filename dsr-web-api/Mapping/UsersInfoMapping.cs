using System;
using dsr_web_api.Dtos;
using dsr_web_api.Models;

namespace dsr_web_api.Mapping;

public static class UsersInfoMapping
{
    // Convert CreateUsersInfoDto → UsersInfo entity
    public static UsersInfo ToEntity(this CreateUsersInfoDto usersDto)
    {
        return new UsersInfo()
        {
            Id = usersDto.Id,
            FirtName = usersDto.FirtName!,
            LastName = usersDto.LastName!,
            Email = usersDto.Email!,
            Mobile = usersDto.Mobile!,
            UserRoleId = usersDto.UserRoleId,
            UserName = usersDto.UserName!,
            PasswordHash = usersDto.PasswordHash!,
            CreatedBy = usersDto.CreatedBy,
            CreatedOn = usersDto.CreatedOn,
            IsActive = usersDto.IsActive,
            IsDeleted = usersDto.IsDeleted,
            LastLoginDateTime = usersDto.LastLoginDateTime,
            LoginDateTime = usersDto.LoginDateTime
        };
    }

    // Convert UpdateUsersInfoDto → UsersInfo entity (with ID)
    public static UsersInfo ToEntity(this UpdateUsersInfoDto usersDto, int id, UsersInfo existingUser)
    {
        return new UsersInfo()
        {
            Id = id,
            FirtName = usersDto.FirtName!,
            LastName = usersDto.LastName!,
            Email = usersDto.Email!,
            Mobile = usersDto.Mobile!,
            UserRoleId = usersDto.UserRoleId,
            UserName = usersDto.UserName!,
            PasswordHash = usersDto.PasswordHash!,
            CreatedBy = existingUser.CreatedBy,
            CreatedOn = existingUser.CreatedOn,
            IsActive = usersDto.IsActive,
            IsDeleted = usersDto.IsDeleted,
            LastLoginDateTime = usersDto.LastLoginDateTime,
            LoginDateTime = usersDto.LoginDateTime,
            UpdatedBy = usersDto.UpdatedBy,
            UpdatedOn = usersDto.UpdatedOn
        };
    }

    // Convert users → Details DTO for GET /users/{id}
    public static UsersInfoDetailsDto ToUsersInfoDetailsDto(this UsersInfo users)
    {
        return new UsersInfoDetailsDto(
            users.Id,
            users.FirtName,
            users.LastName,
            users.Email,
            users.Mobile,
            users.UserRoleId,
            users.UserName,
            users.PasswordHash,
            users.IsActive,
            users.LoginDateTime,
            users.LastLoginDateTime,
            users.IsDeleted,
            users.CreatedBy,
            users.CreatedOn,
            users.UpdatedBy,
            users.UpdatedOn
        );
    }
}

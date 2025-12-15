using System;

namespace dsr_admin.Models;

public class UserSession
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int UserRoleId { get; set; }
    public bool IsLoggedIn => !string.IsNullOrEmpty(UserName);
}

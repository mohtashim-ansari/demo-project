using System;
using dsr_admin.Dtos;

namespace dsr_admin.Models;

public class LoginResponse
{
    public string Message { get; set; } = string.Empty;
    public UserDto? User { get; set; }
}

using System;
using Microsoft.AspNet.Identity;

namespace dsr_web_api.Helpers;

public class PasswordHelper
{
    private readonly IPasswordHasher passwordHasher = new PasswordHasher();

    public string HashPassword(string plainPassword)
    {
        return passwordHasher.HashPassword(plainPassword);
    }

    public bool VerifyPassword(string hashedPassword, string inputPassword)
    {
        var result = passwordHasher.VerifyHashedPassword(hashedPassword, inputPassword);

        return result == PasswordVerificationResult.Success;
    }
}

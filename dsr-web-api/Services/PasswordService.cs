using System;
using dsr_web_api.Models;
using Microsoft.AspNetCore.Identity;

namespace dsr_web_api.Services;

public class PasswordService
{
    private readonly PasswordHasher<UsersInfo> _hasher;

    public PasswordService()
    {
        _hasher = new PasswordHasher<UsersInfo>();
    }

    // -------------------------------
    // HASH PASSWORD
    // -------------------------------
    public string Hash(string password)
    {
        return _hasher.HashPassword(null, password);
    }

    // -------------------------------
    // VERIFY PASSWORD
    // -------------------------------
    public bool Verify(UsersInfo usersInfo,string hashedPassword, string inputPassword)
    {

        var result = _hasher.VerifyHashedPassword(usersInfo, hashedPassword, inputPassword);

        return result == PasswordVerificationResult.Success;
    }
}

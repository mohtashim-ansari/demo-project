using dsr_admin.Dtos;
using dsr_admin.Models;

namespace dsr_admin.Clients;

public class RegisterUser
{
    private readonly HttpClient _http;

    public RegisterUser(HttpClient http)
    {
        _http = http;
    }

    public async Task RegisterUserAsync(CreateUsersInfoDto dto)
    {
        var response = await _http.PostAsJsonAsync("usersinfo", dto);

        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            throw new Exception(msg);
        }
    }

    // GET all Roles
    public async Task<List<RoleResponse>> GetAllRolesAsync()
    {
        var result = await _http.GetFromJsonAsync<List<RoleResponse>>("role");
        return result ?? new List<RoleResponse>();
    }
}

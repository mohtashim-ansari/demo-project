using dsr_admin.Dtos;

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
}

using dsr_admin.Models;
using Newtonsoft.Json;

namespace dsr_admin.Clients;

public class AccountClient(HttpClient httpClient)
{
    public async Task<string> LoginAsync(string userName, string password)
    {
        var request = new LoginRequest
        {
            UserName = userName,
            Password = password
        };

        var response = await httpClient.PostAsJsonAsync("account/login", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(error);
            throw new Exception($"Login failed, {obj.message}");
        }
        else
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            var todaysAttendanceResponse = await httpClient.GetAsync($"attandanceinfo/todays/{result!.User!.Id}");

            if (!todaysAttendanceResponse.IsSuccessStatusCode)
            {
                var attendanceRequest = new AttendanceRequest
                {
                    UserId = result!.User!.Id,
                    AttandanceDate = DateTime.Now,
                    IsPresent = true,
                    IsDSRSent = false,
                    IsDeleted = false,
                    CreatedBy = result.User.Id,
                    CreatedOn = DateTime.Now
                };

                var attendanceResponse = await httpClient.PostAsJsonAsync("attandanceinfo", attendanceRequest);
                if (!attendanceResponse.IsSuccessStatusCode)
                {
                    var error = await attendanceResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Attendance failed.");
                }
            }
            return result!.Message;
        }
    }

}

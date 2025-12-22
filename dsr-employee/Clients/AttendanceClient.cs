using System.Net.Http.Json;
using dsr_employee.Models;

namespace dsr_employee.Clients;

public class AttendanceClient
{
    private readonly HttpClient _http;

    public AttendanceClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<TodaysAttendanceResponse>> GetTodaysAttendanceAsync()
    {
        var response = await _http.GetAsync("/attandanceinfo/todays/all");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<TodaysAttendanceResponse>>() ?? [];
    }

    public async Task SendDSRSentAsync(int attendanceId)
    {
        var response = await _http.PutAsync("/attandanceinfo/dsrsent",
            JsonContent.Create(new { Id = attendanceId }));

        response.EnsureSuccessStatusCode();
    }
}

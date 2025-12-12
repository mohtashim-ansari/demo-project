using System.Net.Http.Json;
using dsr_admin.Models;

namespace dsr_admin.Clients;

public class TodaysClient
{
    private readonly HttpClient _http;

    public TodaysClient(HttpClient http)
    {
        _http = http;
    }

    // GET all users (existing)
    public async Task<List<TodaysAttendanceResponse>> GetTodaysAttendanceAsync()
    {
        var result = await _http.GetFromJsonAsync<List<TodaysAttendanceResponse>>("attandanceinfo/todays/all");
        return result ?? new List<TodaysAttendanceResponse>();
    }    
}

using System.Net.Http.Json;
using dsr_admin.Models;

namespace dsr_admin.Clients;

public class AttendanceClient
{
    private readonly HttpClient _http;

    public AttendanceClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<TodaysAttendanceResponse>> GetTodaysAttendanceAsync()
    {
        var url = "attandanceinfo/todays/all";

        return await _http.GetFromJsonAsync<List<TodaysAttendanceResponse>>(url)
               ?? new List<TodaysAttendanceResponse>();
    }

    public async Task<TodaysAttendanceResponse?> GetAttendanceByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<TodaysAttendanceResponse>(
            $"attandanceinfo/{id}");
    }

    public async Task UpdateAttendanceAsync(TodaysAttendanceResponse model)
    {
        var attendanceinfo = new AttendanceInfo
        {
            Id = model.Id,
            AttandanceDate = model.AttandanceDate
        };
        await _http.PutAsJsonAsync($"attandanceinfo/update", attendanceinfo);
    }

    // New method to get all attendance records
    public async Task<List<TodaysAttendanceResponse>> GetAllAttendanceAsync()
    {
        var url = "attandanceinfo/all";

        return await _http.GetFromJsonAsync<List<TodaysAttendanceResponse>>(url)
               ?? new List<TodaysAttendanceResponse>();
    }

    // New method to search attendance with filters
    public async Task<List<TodaysAttendanceResponse>> SearchAttendanceWithFiltersAsync(
    string? name = null,
    DateTime? fromDate = null,
    DateTime? toDate = null)
    {
        var query = new List<string>();

        if (!string.IsNullOrWhiteSpace(name))
            query.Add($"name={Uri.EscapeDataString(name)}");

        if (fromDate.HasValue)
            query.Add($"fromDate={fromDate:yyyy-MM-dd}");

        if (toDate.HasValue)
            query.Add($"toDate={toDate:yyyy-MM-dd}");

        var url = "attandanceinfo/all/search";
        if (query.Any()) url += "?" + string.Join("&", query);

        return await _http.GetFromJsonAsync<List<TodaysAttendanceResponse>>(url)
            ?? new();
    }

    public async Task SendDSRReminderAsync(TodaysAttendanceResponse model)
    {
        var attendanceinfo = new AttendanceInfo
        {
            Id = model.Id,
            AttandanceDate = model.AttandanceDate
        };
        await _http.PutAsJsonAsync($"attandanceinfo/dsrreminder", attendanceinfo);
    }

}

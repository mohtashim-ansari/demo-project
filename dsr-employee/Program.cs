using dsr_employee.Components;
using dsr_employee.Clients;

var builder = WebApplication.CreateBuilder(args);

// Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 🔐 Read API Key from config
var apiKey = builder.Configuration["ApiKey"];
builder.Services.AddHttpClient<AttendanceClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5254"); // ✅ API HTTP URL
    client.DefaultRequestHeaders.Add(
        "x-api-key",
        builder.Configuration["ApiKey"]
    );
});



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute(
    "/not-found",
    createScopeForStatusCodePages: true
);

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

using dsr_admin.Clients;
using dsr_admin.Components;
using dsr_admin.Models;

var builder = WebApplication.CreateBuilder(args);

// Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var config = builder.Configuration;

builder.Services.AddScoped<UserSession>();

// ----------------------------------------
// ✔ Register TodaysClient
// ----------------------------------------
builder.Services.AddHttpClient<TodaysClient>(client =>
{
    client.BaseAddress = new Uri(config["DSRApiUrl"]!);
    client.DefaultRequestHeaders.Add("X-Api-Key", config["ApiKey"]!);
});

// ----------------------------------------
// ✔ Register AccountClient
// ----------------------------------------
builder.Services.AddHttpClient<AccountClient>(client =>
{
    client.BaseAddress = new Uri(config["DSRApiUrl"]!);
    client.DefaultRequestHeaders.Add("X-Api-Key", config["ApiKey"]!);
});

// ----------------------------------------
// ✔ Register RegisterUser
// ----------------------------------------
builder.Services.AddHttpClient<RegisterUser>(client =>
{
    client.BaseAddress = new Uri(config["DSRApiUrl"]!);
    client.DefaultRequestHeaders.Add("X-Api-Key", config["ApiKey"]!);
});


// ----------------------------------------
// Build app
// ----------------------------------------
var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

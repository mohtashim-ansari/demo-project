using dsr_admin.Clients;
using dsr_admin.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ✔️ Bring config **before** using it
var config = builder.Configuration;

// ----------------------------------------
// Register TodaysClient
// ----------------------------------------
builder.Services.AddHttpClient<TodaysClient>(client =>
{
    client.BaseAddress = new Uri(config["DSRApiUrl"]!);
    client.DefaultRequestHeaders.Add("X-Api-Key", config["ApiKey"]);
});

// ----------------------------------------
// Register AccountClient
// ----------------------------------------
builder.Services.AddHttpClient<AccountClient>(client =>
{
    client.BaseAddress = new Uri(config["DSRApiUrl"]!);
    client.DefaultRequestHeaders.Add("X-Api-Key", config["ApiKey"]);
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

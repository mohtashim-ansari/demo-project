using dsr_admin.Clients;
using dsr_admin.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register HttpClient
var config = builder.Configuration;

var DSRApiUrl = builder.Configuration["DSRApiUrl"] ??
throw new Exception("DSRApiUrl Is Not Set");

// Register AccountClient
builder.Services.AddHttpClient<AccountClient>(client =>
{
    client.BaseAddress = new Uri(config["DSRApiUrl"]!);
    client.DefaultRequestHeaders.Add("X-Api-Key", config["ApiKey"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

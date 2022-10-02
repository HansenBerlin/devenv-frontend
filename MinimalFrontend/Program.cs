using Microsoft.Fast.Components.FluentUI;
using MinimalFrontend.Controller;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddFluentUIComponents();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IUserRepositoryController>(sp => new UserRepositoryController(
    sp.GetService<HttpClient>()!, sp.GetService<IConfiguration>()!["UsersEndpoint"]));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
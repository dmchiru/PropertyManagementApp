using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PropertyManagementApp.Client;

using PropertyManagementApp.Client.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<NotificationService>();


builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7108/")
});

await builder.Build().RunAsync();
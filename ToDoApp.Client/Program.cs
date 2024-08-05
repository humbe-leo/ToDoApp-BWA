using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDoApp.Client;
using ToDoApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


string ApiUrl = builder.Configuration.GetValue<string>(nameof(ApiUrl)) ?? builder.HostEnvironment.BaseAddress;
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(ApiUrl) });

builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<ISubTaskService, SubTaskService>();

await builder.Build().RunAsync();

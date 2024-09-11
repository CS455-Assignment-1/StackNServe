using System;
using System.Net.Http;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using StackNServe;
using StackNServe.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<GlobalStringListService>();

// Replacing the misplaced end-of-line marker with two newlines
builder.Services.AddScoped<SelectionButtonService>();

await builder.Build().RunAsync();

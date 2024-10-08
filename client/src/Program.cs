using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using StackNServe.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace StackNServe
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // Create an HttpClient to load appsettings.json
            var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

            // Load appsettings.json
            var response = await httpClient.GetAsync("appsettings.json");
            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync();
                builder.Configuration.AddJsonStream(stream);
            }
            else
            {
                throw new InvalidOperationException("Failed to load configuration.");
            }

            // Dynamically set the Base URL
            string apiBaseUrl;
            if (builder.HostEnvironment.IsDevelopment())
            {
                // Use Local URL during development
                apiBaseUrl = builder.Configuration["ApiSettings:LocalBaseUrl"];
            }
            else
            {
                // Use Production URL when hosted
                apiBaseUrl = builder.Configuration["ApiSettings:ProductionBaseUrl"];
            }

            Console.WriteLine($"Using API Base URL: {apiBaseUrl}");

            if (string.IsNullOrWhiteSpace(apiBaseUrl))
            {
                throw new InvalidOperationException("API Base URL is not configured.");
            }

            // Register HttpClient with the selected base URL
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

            // Register other services
            builder.Services.AddScoped<GlobalStringListService>();
            builder.Services.AddScoped<SelectionButtonService>();

            await builder.Build().RunAsync();
        }
    }
}

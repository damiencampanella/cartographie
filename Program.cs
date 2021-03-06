using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Globalization;
using System.Threading.Tasks;
using BlazorFlowly.Services;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorFlowly.Models;

namespace BlazorFlowly
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IMapDataService, MapDataService>();
            builder.Services.AddScoped<IStopTimesDataService, StopTimesDataService>();
            builder.Services.AddScoped<IPositionsDataService, PositionsDataService>();

            var jsInterop = builder.Build().Services.GetRequiredService<IJSRuntime>();
            var appLanguage = await jsInterop.InvokeAsync<string>("appCulture.get");
            if (appLanguage != null)
            {
                CultureInfo cultureInfo = new(appLanguage);
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            }

            await builder.Build().RunAsync();
        }
    }
}
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShiftPlanner.Client;
using ShiftPlanner.Client.Services;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IShiftService, ShiftService>();

//builder.Services.AddScoped<IClipboardService, ClipboardService>();
//builder.Services.AddScoped<IDialogService, DialogService>();

//builder.Services.AddScoped<ICustomerService, CustomerService>();

//builder.Services.AddTokenAuthentication();
//builder.Services.AddScoped<IAuthService, AuthService>();

SyncfusionLicenseProvider.RegisterLicense("NTI1MjA1QDMxMzkyZTMzMmUzMFZnNDg2ajMzNllwSHVpWUNDTXhWTndzZ3U4OGJtazhVN0JpbXN5OUkvMk09");
builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });


await builder.Build().RunAsync();


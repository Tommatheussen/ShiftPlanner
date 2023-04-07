using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ShiftPlanner.Client;
using ShiftPlanner.Client.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddSingleton<IAppStateService, AppStateService>();


//builder.

//builder.Services.AddScoped<IClipboardService, ClipboardService>();
//builder.Services.AddScoped<IDialogService, DialogService>();

//builder.Services.AddScoped<ICustomerService, CustomerService>();

//builder.Services.AddTokenAuthentication();
//builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();


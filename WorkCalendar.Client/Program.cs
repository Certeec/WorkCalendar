using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WorkCalendar.Client;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using WorkCalendar.Client.Data.Scheduler;
using WorkCalendar.Client.Data;
using WorkCalendar.Client.Data.Accounts;
using WorkCalendar.Client.Data.Scheduler.SchedulerPlaces;
using WorkCalendar.Client.Data.Scheduler.SchedulerGenerator;
using WorkCalendar.Client.Data.Auth;
using WorkCalendar.Client.Data.MessageBox;
using WorkCalendar.Client.Data.Scheduler.SchedulerUserDefaults;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Konfiguracja root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient configuration
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Dodatkowy HttpClient dla zewnêtrznych API (jeœli potrzebny)
builder.Services.AddHttpClient();

// Twoje serwisy
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<IUserActions, UserActions>();
builder.Services.AddSingleton<IMessageBoxHandler, MessageBoxHandler>();
builder.Services.AddScoped<IUserLogsActions, UserLogsActions>();
builder.Services.AddTransient<ISchedulerTaskService, SchedulerTaskService>();
builder.Services.AddTransient<ISchedulerPlacesService, SchedulerPlacesService>();
builder.Services.AddTransient<ISchedulerDefaultHourIncomeService, SchedulerDefaultHourIncomeService>();
builder.Services.AddTransient<ISchedulerService, SchedulerService>();
builder.Services.AddTransient<ISchedulerGeneratorService, SchedulerGeneratorService>();

// Authentication & Authorization
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

// External libraries
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);



await builder.Build().RunAsync();
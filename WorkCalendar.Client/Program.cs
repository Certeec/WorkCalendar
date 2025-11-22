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
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var serverAddress = builder.Configuration["ServerAdress"] ?? "/api/";

Uri apiAddress;
if (serverAddress.StartsWith("http://") || serverAddress.StartsWith("https://"))
{
    apiAddress = new Uri(serverAddress);
}
else
{
    var baseUri = new Uri(builder.HostEnvironment.BaseAddress);
    apiAddress = new Uri(baseUri, serverAddress);
}

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = apiAddress;
    client.Timeout = TimeSpan.FromMinutes(2); // Krótszy timeout dla stabilności

    // Dodaj headers dla lepszej kompatybilności
    client.DefaultRequestHeaders.Add("User-Agent", "BlazorWASM/1.0");
});

// Główny HttpClient
builder.Services.AddScoped(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var client = httpClientFactory.CreateClient("API");
    return client;
});

// POPRAWIONE lifetime management
builder.Services.AddAuthorizationCore();

// Serwisy związane z użytkownikami - Scoped (per user session)
builder.Services.AddScoped<IUserActions, UserActions>();
builder.Services.AddScoped<IUserLogsActions, UserLogsActions>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

// Serwisy biznesowe - Scoped (współdzielone w ramach requesta/komponentu)
builder.Services.AddScoped<ISchedulerTaskService, SchedulerTaskService>();
builder.Services.AddScoped<ISchedulerPlacesService, SchedulerPlacesService>();
builder.Services.AddScoped<ISchedulerDefaultHourIncomeService, SchedulerDefaultHourIncomeService>();
builder.Services.AddScoped<ISchedulerService, SchedulerService>();
builder.Services.AddScoped<ISchedulerGeneratorService, SchedulerGeneratorService>();

// MessageBox jako Scoped zamiast Singleton - uniknie memory leaks
builder.Services.AddScoped<IMessageBoxHandler, MessageBoxHandler>();

// Standardowe serwisy
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices(config =>
{
    // Opcjonalna konfiguracja MudBlazor
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
});

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


// Konfiguracja HttpClient timeout jest już ustawiona wyżej

var app = builder.Build();


// Dodaj error handling
try
{
    await app.RunAsync();
}
catch (Exception ex)
{
    // Log error - możesz dodać tutaj logging
    Console.WriteLine($"Application failed to start: {ex.Message}");
    throw;
}
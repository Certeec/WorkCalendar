using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WorkCalendar.Library.Accounts;
using WorkCalendar.Library.Accounts.Safety;
using WorkCalendar.Library.Accounts.UserHistory;
using WorkCalendar.Library.Planner;
using WorkCalendar.Library.Planner.ConfigurableDefaultvalues;
using WorkCalendar.Library.Planner.Places;
using WorkCalendar.Library.Planner.SchedulerGenerator;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja Windows Service
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "WorkCalendar API Service";
});

builder.Services.AddControllers();

// Dodanie konfiguracji CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5083", "https://localhost:7198")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IAccountsRepository, EFAccountsRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IHasher, Sha256Hasher>();
builder.Services.AddTransient<IUserLogsRepository, EFUserLogsRepository>();
builder.Services.AddTransient<IWorkPlannerService, WorkPlannerService>();
builder.Services.AddTransient<IUserSchedulerPlacesService, EFUserSchedulerPlacesService>();
builder.Services.AddTransient<IUserDefaultIncomeService, UserDefaultIncomeService>();
builder.Services.AddTransient<ISchedulerColorService, SchedulerColorService>();
builder.Services.AddTransient<ISchedulerGeneratorService, SchedulerGeneratorService>();
builder.Services.AddTransient<ISchedulerGeneratorRepository, SchedulerGeneratorRepository>();
builder.Services.AddScoped<DatabaseContext>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = "Bearer";
    opt.DefaultChallengeScheme = "Bearer";
})
.AddJwtBearer(opt =>
{   // for development only
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["JWT:SecretKey"])),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"]
    };
});
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Database initialization and migration
await InitializeDatabaseAsync(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "openapi/v1.json";
    });
}

app.UseHttpsRedirection();

// Serwowanie statycznych plików Blazor WebAssembly
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Fallback do index.html dla Blazor routing
app.MapFallbackToFile("index.html");

app.Run();

static async Task InitializeDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {

        logger.LogInformation("Rozpoczynanie migracji bazy danych...");
        await dbContext.Database.MigrateAsync();
        logger.LogInformation("Migracje bazy danych zakończone pomyślnie.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Błąd podczas migracji bazy danych: {Message}", ex.Message);
        throw;
    }
}
using Microsoft.IdentityModel.Tokens;
using WorkCalendar.Library.Accounts;
using WorkCalendar.Library.Accounts.Safety;
using WorkCalendar.Library.Accounts.UserHistory;
using WorkCalendar.Library.Planner;
using WorkCalendar.Library.Planner.ConfigurableDefaultvalues;
using WorkCalendar.Library.Planner.Places;
using WorkCalendar.Library.Planner.SchedulerGenerator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IAccountsRepository, AccountsRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IHasher, Sha256Hasher>();
builder.Services.AddTransient<IUserLogsRepository, UserLogsRepository>();
builder.Services.AddTransient<IWorkPlannerService, WorkPlannerService>();
builder.Services.AddTransient<IWorkPlannerRepository, WorkPlannerRepository>();
builder.Services.AddTransient<IUserSchedulerPlacesService, UserSchedulerPlacesService>();
builder.Services.AddTransient<IUserDefaultIncomeService, UserDefaultIncomeService>();
builder.Services.AddTransient<IUserDefaultIncomeRepository, UserDefaultIncomeRepository>();
builder.Services.AddTransient<ISchedulerColorService, SchedulerColorService>();
builder.Services.AddTransient<ISchedulerGeneratorService, SchedulerGeneratorService>();
builder.Services.AddTransient<ISchedulerGeneratorRepository, SchedulerGeneratorRepository>();

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7001", "http://localhost:7001")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("BlazorClient");

app.MapControllers();

app.Run();

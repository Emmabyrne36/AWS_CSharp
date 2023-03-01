using Weather.Api.Models;
using Weather.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var env = builder.Environment.EnvironmentName;
var appName = builder.Environment.ApplicationName;

// This configuration is based of a key with format: Development_Weather.Api_OpenWeatherMapApi__ApiKey
builder.Configuration.AddSecretsManager(configurator: options =>
{
    options.SecretFilter = entry => entry.Name.StartsWith($"{env}_{appName}");
    options.KeyGenerator = (_, s) => s
        .Replace($"{env}_{appName}_", string.Empty)
        .Replace("__", ":");
    options.PollingInterval = TimeSpan.FromDays(1);
});

builder.Services.Configure<OpenWeatherApiSettings>(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IWeatherService, WeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

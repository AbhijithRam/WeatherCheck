// Changed code below - replaces the minimal app above with a fuller setup
// filepath: /home/abhijithramp/Desktop/TestNETProject/Project_V1/Project_V1/Program.cs
using MyDotnet8Api.Interfaces;
using MyDotnet8Api.Models;
using MyDotnet8Api.Repositories;
using MyDotnet8Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register generic in-memory repository and WeatherService
builder.Services.AddSingleton(typeof(IRepository<>), typeof(InMemoryRepository<>));
builder.Services.AddScoped<IWeatherService, WeatherService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

// Seed some data on startup
using (var scope = app.Services.CreateScope())
{
    var repo = (IRepository<WeatherForecast>)scope.ServiceProvider.GetRequiredService(typeof(IRepository<WeatherForecast>));
    await repo.AddAsync(new WeatherForecast { Date = DateTime.UtcNow, TemperatureC = 20, Summary = "Sunny" });
    await repo.AddAsync(new WeatherForecast { Date = DateTime.UtcNow.AddDays(1), TemperatureC = 15, Summary = "Cloudy" });
}

app.Run();

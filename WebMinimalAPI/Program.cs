using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<DbContext>("Data Source=(local);Database=aot;Integrated security=true;TrustServerCertificate=true");
builder.Services.ConfigureHttpJsonOptions(x =>
{

    x.SerializerOptions.IgnoreReadOnlyProperties = true;
    x.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    x.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Add services to the container.

var app = builder.Build();


// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", /*[ServiceFilter<>()]*/ () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public int DefaultInt { get; set; }
    public string DefaultString { get; set; }
    public float ReadOnlyFloat => 50;
}

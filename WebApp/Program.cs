using Microsoft.AspNetCore.Authorization;
using WebApp.SignalR;
using SignalRSwaggerGen;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebAPI", Version = "v1" });
    x.AddSignalRSwaggerGen();
});

var app = builder.Build();

app.Use(async (httpContext, next) => {

    Console.WriteLine( "Before Use1");

    await next(httpContext);

    Console.WriteLine("After Use1");
});

app.Use(async (httpContext, next) => {

    Console.WriteLine("Before Use2");

    await next(httpContext);

    Console.WriteLine("After Use2");
});

var values = new List<int>
{
    1, 5, 8, 23, 456
};


app.UseSwagger();
app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerWebAPI v1"); });

//MinimalAPI
app.MapGet("/values", () => values);
app.MapDelete("/values/{value:int}", /*[Authorize]*/ (int value) => values.Remove(value));
app.MapPost("/values/{value:int}", (int value) => values.Add(value));
app.MapPut("/values/{oldValue:int}/{newValue:int}", (int oldValue, int newValue) => values[values.IndexOf(oldValue)] = newValue);

app.MapHub<DemoHub>("SignalR/Demo");

app.Run();

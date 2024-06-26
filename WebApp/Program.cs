using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);


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

//MinimalAPI
app.MapGet("/values", () => values);
app.MapDelete("/values/{value:int}", /*[Authorize]*/ (int value) => values.Remove(value));
app.MapPost("/values/{value:int}", (int value) => values.Add(value));
app.MapPut("/values/{oldValue:int}/{newValue:int}", (int oldValue, int newValue) => values[values.IndexOf(oldValue)] = newValue);

app.Run();

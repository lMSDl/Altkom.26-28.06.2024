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

app.MapGet("/", () => "Hello World!");


app.Run();

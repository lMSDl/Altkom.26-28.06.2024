using GrpcService.Services;
using Services.Bogus.Fakers;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();


builder.Services.AddTransient<EntityFaker<Models.Person>, PersonFaker>();
builder.Services.AddSingleton<IPeopleService, Services.Bogus.PeopleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<PeopleService>();
app.MapGrpcService<StreamService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using WebApi.Validatiors;
using WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    /*.AddJsonOptions( x =>
    {
        x.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
        x.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    })*/


    .AddNewtonsoftJson(x =>
    {
        x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        x.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
        x.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        x.SerializerSettings.DateFormatString = "yyy MM d hh:ss;mm";
    })
    .AddXmlSerializerFormatters(); //wsparcie dla XML


builder.Services.AddSingleton<IList<int>>(x => [2, 3, 5, 1, 23]);
builder.Services.AddTransient<EntityFaker<ShoppingList>, ShoppingListFaker>();
builder.Services.AddTransient<EntityFaker<Person>, PersonFaker>();
builder.Services.AddTransient<EntityFaker<Product>, ProductFaker>();
builder.Services.AddSingleton<ICRUDService<ShoppingList>, CRUDService<ShoppingList>>();
builder.Services.AddSingleton<IPeopleService, PeopleService>();
builder.Services.AddSingleton<ICRUDChildService<Product>, CRUDChildService<Product>>();

builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddTransient<ConsoleLogFilter>();
builder.Services.AddTransient<UniquePersonFilter>();
builder.Services.AddSingleton(x => new LimiterFilter(5));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebAPI", Version = "v1" }))
    .AddSwaggerGenNewtonsoftSupport();

//rêczna rejestracja walidatorów
//builder.Services.AddTransient<IValidator<ShoppingList>, ShoppingListValidator>();

//zawieszenie automatycznej walidacji modelu
//builder.Services.Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerWebAPI v1"));
app.MapControllers();

app.Run();

//GET api/shoppingLists/3/products
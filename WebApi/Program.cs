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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions( x =>
    {
        x.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
        x.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    })

    /*.AddNewtonsoftJson(x =>
    {
        x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        x.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
        x.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        x.SerializerSettings.DateFormatString = "yyy MM d hh:ss;mm";
    })*/
    .AddXmlSerializerFormatters(); //wsparcie dla XML



builder.Services.AddSingleton<IList<int>>(x => [2, 3, 5, 1, 23]);
builder.Services.AddTransient<EntityFaker<ShoppingList>, ShoppingListFaker>();
builder.Services.AddTransient<EntityFaker<Person>, PersonFaker>();
builder.Services.AddTransient<EntityFaker<Product>, ProductFaker>();
builder.Services.AddSingleton<ICRUDService<ShoppingList>, CRUDService<ShoppingList>>();
builder.Services.AddSingleton<IPeopleService, PeopleService>();
builder.Services.AddSingleton<ICRUDChildService<Product>, CRUDChildService<Product>>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<IValidator<ShoppingList>, ShoppingListValidator>();

//zawieszenie automatycznej walidacji modelu
//builder.Services.Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();


app.Run();


//GET api/shoppingLists/3/products
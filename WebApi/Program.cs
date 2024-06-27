using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IList<int>>(x => [2, 3, 5, 1, 23]);
builder.Services.AddTransient<EntityFaker<ShoppingList>, ShoppingListFaker>();
builder.Services.AddTransient<EntityFaker<Person>, PersonFaker>();
builder.Services.AddTransient<EntityFaker<Product>, ProductFaker>();
builder.Services.AddSingleton<ICRUDService<ShoppingList>, CRUDService<ShoppingList>>();
builder.Services.AddSingleton<IPeopleService, PeopleService>();
builder.Services.AddSingleton<ICRUDChildService<Product>, CRUDChildService<Product>>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();


app.Run();


//GET api/shoppingLists/3/products
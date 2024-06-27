

using ConsoleApp;
using Microsoft.VisualBasic;
using Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
httpClient.BaseAddress = new Uri("http://localhost:5087/api/");


var response = await httpClient.GetAsync("shoppingLists");

/*if(response.StatusCode != System.Net.HttpStatusCode.OK)
{
    Console.WriteLine($"Status not OK ({response.StatusCode})");
    return;
}*/

/*if(!response.IsSuccessStatusCode)
{
    Console.WriteLine($"Status not success ({response.StatusCode})");
    return;
}*/

response.EnsureSuccessStatusCode();

var options = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
var shoppingLists = await response.Content.ReadFromJsonAsync < IEnumerable < ShoppingList >> (options);

foreach (var item in shoppingLists)
{
    Console.WriteLine(item.Name);
}


var product = new Product() { Name = "Toy", Price = 12.2f };

response = await httpClient.PostAsJsonAsync($"shoppingLists/{shoppingLists.First().Id}/Products", product, options);

response.EnsureSuccessStatusCode();

product = await response.Content.ReadFromJsonAsync<Product>(options);

Console.WriteLine(product.Id);

var webApiClient = new WebApiClient("http://localhost:5087/api/");

product = await webApiClient.GetAsync<Product>($"Products/{product.Id}");

Console.WriteLine(product.Name);


Console.ReadLine();


httpClient = new HttpClient();
var webapi = new MyNamespace.MyClass("http://localhost:5087/", httpClient);
foreach (var item in await webapi.PeopleAllAsync())
{
    Console.WriteLine($"{item.FirstName} {item.LastName}");
}


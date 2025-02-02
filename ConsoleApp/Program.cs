﻿

using ConsoleApp;
using Grpc.Net.Client;
using GrpcService.Protos;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualBasic;
using Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


var channel = GrpcChannel.ForAddress("https://localhost:7259");


var streamClient = new GrpcService.Protos.GrpcStream.GrpcStreamClient(channel);
var downStream = streamClient.FromServer(new GrpcService.Protos.Request { Text = "Hello!" });
var source = new CancellationTokenSource();
var counter = 0;
while (await downStream.ResponseStream.MoveNext(source.Token))
{
    Console.WriteLine($"{counter} {downStream.ResponseStream.Current.Text}");
    counter++;
    if (counter == 1000)
    {
        source.Cancel();
        break;
    }
}

var upStream = streamClient.ToServer();

foreach (var letter in "Im using STREAM!!")
    await upStream.RequestStream.WriteAsync(new GrpcService.Protos.Request { Text = letter.ToString() });

await upStream.RequestStream.CompleteAsync();
var response = await upStream.ResponseAsync;

Console.WriteLine(response.Text);


var streams = streamClient.FromAndToServer();

_ = Task.Run(async () =>
{
    for (int i = 0; i < int.MaxValue; i++)
    {
        await streams.RequestStream.WriteAsync(new GrpcService.Protos.Request { Text = i.ToString() });
    }
    await streams.RequestStream.CompleteAsync();
});

_ = Task.Run(async () =>
{
    while (await streams.ResponseStream.MoveNext(CancellationToken.None))
    {
        Console.WriteLine(streams.ResponseStream.Current.Text);
    }
});



var client  = new GrpcService.Protos.PeopleService.PeopleServiceClient(channel);

var people = await client.ReadAsync(new GrpcService.Protos.Void());

int id = people.Collection.FirstOrDefault().Id;

await client.DeleteAsync(new Id { Value = id });

var person = await client.ReadByIdAsync(new Id { Value = id });

person = await client.CreateAsync(new GrpcService.Protos.Person() { FirstName = "Ewa", LastName = "Ewowska" });




Console.ReadLine();





static async Task WebAPI()
{
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
    var shoppingLists = await response.Content.ReadFromJsonAsync<IEnumerable<ShoppingList>>(options);

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
}

static async Task SignalRDemo()
{
    var signalR = new HubConnectionBuilder().WithUrl("http://localhost:5172/SignalR/Demo")
        .WithAutomaticReconnect().Build();

    //signalR.On<string>("TextMessage", x => TextMessage(x));
    signalR.On<string>("TextMessage", TextMessage);
    signalR.On<MyItem>("HandleItem", HandleItem);

    void HandleItem(MyItem item)
    {
        Console.WriteLine($"{item.Name} {item.Value2} {item.DateTime}");
    }

    signalR.Reconnecting += SignalR_Reconnecting;
    signalR.Reconnected += SignalR_Reconnected;

    Task SignalR_Reconnected(string? arg)
    {
        Console.WriteLine("Connected");
        return Task.CompletedTask;
    }

    Task SignalR_Reconnecting(Exception? arg)
    {
        if (arg is not null)
        {
            Console.WriteLine(arg.Message);
        }
        Console.WriteLine("Reconnecting...");
        return Task.CompletedTask;
    }

    void TextMessage(string message)
    {
        Console.WriteLine(message);
    }

    await signalR.StartAsync();

    await signalR.SendAsync("SayHelloToOthers", $"Hello my name is {signalR.ConnectionId}");

    while (true)
    {
        var groupName = Console.ReadLine();

        await signalR.SendAsync("JoinGroup", groupName);
    }
}

static async Task SignalR()
{
    var signalR = new HubConnectionBuilder().WithUrl("http://localhost:5087/SignalR/shoppinglists")
            .WithAutomaticReconnect().Build();

    signalR.On<Product>("NewProductOnList", NewProductOnList);

    void NewProductOnList(Product product)
    {
        Console.WriteLine($"Nowy produkt: {product.Name}");
    }


    await signalR.StartAsync();

    await signalR.SendAsync("NewProductOnList", 3);

    await signalR.SendAsync("Join", Console.ReadLine());
}

class MyItem
{
    public string Name { get; set; }
    public int Value2 { get; set; }
    public DateTime DateTime { get; set; }
}
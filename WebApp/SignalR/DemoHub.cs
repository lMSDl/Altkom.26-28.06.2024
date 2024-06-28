using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace WebApp.SignalR
{
    [SignalRHub]
    public class DemoHub : Hub
    {

        public override async Task OnConnectedAsync()
        {
            base.OnConnectedAsync();


            Console.WriteLine(Context.ConnectionId);

            await Clients.Caller.SendAsync("TextMessage", "Welcome in signalR");

            await Clients.Caller.SendAsync("HandleItem", new Item() { Name = "ItemName", Value = 213, DateTime = DateTime.Now.AddDays(-123) });
        }

        public Task DoSth(Item item)
        {
            return Task.CompletedTask;
        }

        public Task SayHelloToOthers(string message)
        {
            return Clients.Others.SendAsync("TextMessage", message);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Groups(groupName).SendAsync("TextMessage", $"New member in group {groupName}: {Context.ConnectionId}");
        }


        public class Item
        {
            public string Name { get; set; }
            public int Value { get; set; }
            public DateTime DateTime { get; set; }
        }
    }
}

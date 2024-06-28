using Microsoft.AspNetCore.SignalR;
using Models;
using Services.Interfaces;

namespace WebApi.Hubs
{
    public class ShoppingListsHub(ICRUDChildService<Product> productsService) : Hub
    {
        //internal/private/protected - blokuje dostęp do metod dla RPC
        internal async Task NewProductOnList(string listName, int productId)
        {
            await Clients.Group(listName).SendAsync("NewProductOnList", await productsService.ReadAsync(productId));
        }

        public async Task Join(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using Services.Interfaces;
using WebApi.Filters;
using WebApi.Hubs;

namespace WebApi.Controllers
{

    [ServiceFilter<ConsoleLogFilter>]
    public class ProductsController(ICRUDChildService<Product> service, ICRUDService<ShoppingList> parentService, ShoppingListsHub shoppinglistHub) : CRUDChildController<Product, ShoppingList>(service, parentService)
    //public class ProductsController(ICRUDChildService<Product> service, ICRUDService<ShoppingList> parentService, IHubContext<ShoppingListsHub> shoppinglistHub) : CRUDChildController<Product, ShoppingList>(service, parentService)
    {
        [HttpGet("/api/ShoppingLists/{parentId}/Products")]
        public override Task<IActionResult> GetAll(int parentId)
        {
            return base.GetAll(parentId);
        }

        [HttpPost("/api/ShoppingLists/{parentId}/Products")]
        public override async Task<IActionResult> Post(int parentId, Product item)
        {

            //Ręczna walidacja modelu
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var items = ((await GetAll(parentId)) as OkObjectResult)?.Value as IEnumerable<Product>;
            if (items.Any(x => x.Name == item.Name))
            {
                //ręczne dodanie błędu walidacji
                ModelState.AddModelError(nameof(Product.Name), "Ten produkt już znajduje się na liście zakupowej");

                return BadRequest(ModelState);
            }

            var result = await base.Post(parentId, item);

            if(result is CreatedAtActionResult)
            {
                var groupName = (await parentService.ReadAsync(parentId))!.Name!;
                await shoppinglistHub.NewProductOnList(groupName, item.Id);
                //await shoppinglistHub.Clients.Group(groupName).SendAsync("NewProductOnList", await service.ReadAsync(item.Id));
            }

            return result;
        }
    }
}

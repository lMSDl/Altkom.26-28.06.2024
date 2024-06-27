using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using WebApi.Filters;

namespace WebApi.Controllers
{

    [ServiceFilter<ConsoleLogFilter>]
    public class ProductsController : CRUDChildController<Product, ShoppingList>
    {
        public ProductsController(ICRUDChildService<Product> service, ICRUDService<ShoppingList> parentService) : base(service, parentService)
        {
        }

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

            return await base.Post(parentId, item);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using System.Diagnostics.Tracing;

namespace WebApi.Controllers
{

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
        public override Task<IActionResult> Post(int parentId, Product item)
        {
            return base.Post(parentId, item);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using System.Diagnostics.Tracing;

namespace WebApi.Controllers
{

    [Route("/api/ShoppingLists/{parentId:int}/Products")]
    public class ProductsController : CRUDChildController<Product, ShoppingList>
    {
        public ProductsController(ICRUDChildService<Product> service, ICRUDService<ShoppingList> parentService) : base(service, parentService)
        {
        }

    }
}

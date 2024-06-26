using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using System.Diagnostics.Tracing;

namespace WebApi.Controllers
{
    public class ProductsController : CrudController<Product>
    {
        private ICrudService<ShoppingList> _parentService;
        private ICrudService<Product> _service;

        public ProductsController(ICrudService<Product> service, ICrudService<ShoppingList> parentService) : base(service)
        {
            _service = service;
            _parentService = parentService;
        }



        [NonAction]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ReadAsync());
        }

        [HttpGet("/api/ShoppingLists/{parentId:int}/[controller]")]
        public async Task<IActionResult> GetByParent(int parentId)
        {
            if (await _parentService.ReadAsync(parentId) is null)
                return NotFound();

            var products = (await _service.ReadAsync()).Where(x => x.ShoppingListId == parentId);

            return Ok(products);
        }
    }
}

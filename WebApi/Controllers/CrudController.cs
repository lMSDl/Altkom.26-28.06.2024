using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    public abstract class CRUDController<T> : RUDController<T> where T : Entity
    {
        private ICRUDService<T> _service;

        protected CRUDController(ICRUDService<T> service) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ReadAllAsync());
        }

        [HttpPost]
        public virtual async Task<IActionResult>Post(T item)
        {
            item = await _service.CreateAsync(item);

            //return Created($"api/ShoppingLists/{id}", shoppingList);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
    }
}

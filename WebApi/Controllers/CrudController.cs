using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    public abstract class CrudController<T> : ApiController where T : Entity
    {
        private ICrudService<T> _service;

        protected CrudController(ICrudService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ReadAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = _service.ReadAsync(id);
            if (item == null)
                return NotFound();

            await Task.Delay(1000);

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult >Post(T item)
        {
            item = await _service.CreateAsync(item);

            //return Created($"api/ShoppingLists/{id}", shoppingList);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, T item)
        {
            var localItem = _service.ReadAsync(id);
            if (localItem == null)
                return NotFound();

            await _service.UpdateAsync(id, item);

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var localItem = _service.ReadAsync(id);
            if (localItem == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}

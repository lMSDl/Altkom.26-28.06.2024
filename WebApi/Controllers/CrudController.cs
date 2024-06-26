using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApi.Controllers
{
    public abstract class CrudController<T> : ApiController where T : Entity
    {
        static protected ICollection<T> _items = new List<T>();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_items);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = _items.SingleOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();

            await Task.Delay(1000);

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post(T item)
        {
            int id = _items.Select(x => x.Id).DefaultIfEmpty(0).Max() + 1;

            item.Id = id;
            _items.Add(item);

            //return Created($"api/ShoppingLists/{id}", shoppingList);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, T item)
        {
            var localItem = _items.SingleOrDefault(x => x.Id == id);
            if (localItem == null)
                return NotFound();

            _items.Remove(localItem);
            item.Id = id;
            _items.Add(item);

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public virtual IActionResult Delete(int id)
        {
            var item = _items.SingleOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();

            _items.Remove(item);

            return NoContent();
        }
    }
}

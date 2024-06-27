using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    public class ShoppingListsController : CRUDController<ShoppingList>
    {
        public ShoppingListsController(ICRUDService<ShoppingList> service) : base(service)
        {
        }

        /*static ICollection<ShoppingList> _shoppingLists = new List<ShoppingList>();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_shoppingLists);
        }
        

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var shoppngList = _shoppingLists.SingleOrDefault(x => x.Id == id);
            if(shoppngList == null)
                return NotFound();

            await Task.Delay(1000);

            return Ok(shoppngList);
        }

        [HttpPost]
        public IActionResult Post(ShoppingList shoppingList)
        {
            int id = _shoppingLists.Select(x => x.Id).DefaultIfEmpty(0).Max() + 1;

            shoppingList.Id = id;
            _shoppingLists.Add(shoppingList);

            //return Created($"api/ShoppingLists/{id}", shoppingList);
            return  CreatedAtAction(nameof(Get), new { id = shoppingList.Id }, shoppingList);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, ShoppingList shoppingList)
        {
            var localShoppngList = _shoppingLists.SingleOrDefault(x => x.Id == id);
            if (localShoppngList == null)
                return NotFound();

            _shoppingLists.Remove(localShoppngList);
            shoppingList.Id = id;
            _shoppingLists.Add(shoppingList);

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var shoppngList = _shoppingLists.SingleOrDefault(x => x.Id == id);
            if (shoppngList == null)
                return NotFound();

            _shoppingLists.Remove(shoppngList);

            return NoContent();
        }*/
    }
}

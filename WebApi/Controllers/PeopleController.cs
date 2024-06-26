using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApi.Controllers
{
    public class PeopleController : CrudController<Person>
    {
        [NonAction] // wyłączenie ankcji z obsługi - usługa zachowuje się tak, jakby ta metoda nie była w ogóle zaimplementowana (kod 405)
        public override IActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        [HttpGet("search")]
        public IActionResult GetByFistName(string firstName)
        {
            var items = _items.Where(x => string.Compare(x.FirstName, firstName, true) == 0).ToList();
            return Ok(items);
        }
    }
}

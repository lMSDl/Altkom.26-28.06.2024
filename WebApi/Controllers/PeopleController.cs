using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    public class PeopleController : CRUDController<Person>
    {
        private IPeopleService _peropleService;
        public PeopleController(IPeopleService service) : base(service)
        {
            _peropleService = service;
        }

        [NonAction] // wyłączenie ankcji z obsługi - usługa zachowuje się tak, jakby ta metoda nie była w ogóle zaimplementowana (kod 405)
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByFistName(string firstName)
        {
            return Ok(await _peropleService.SearchByFirstName(firstName));
        }
    }
}

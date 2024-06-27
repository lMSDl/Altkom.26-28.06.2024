using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{    
    //adnotacje dziedziczone z ApiController
    //[Route("api/[controller]")]
    //[ApiController] 
    public class ValuesController : ApiController
    {
        IList<int> _values;
        public ValuesController(IList<int> values)
        {
            _values = values;
        }

        //GET: localhost:<port>/api/values
        [HttpGet] //oznaczenie metody GET - metoda nie musi nosić w nazwie lub nazywać się "Get"
        [Produces("application/xml")] //wymuszenie odpowiedzi jako XML
        public IEnumerable<int> AlaMaKota()
        {
            return _values;
        }

        //POST: localhost:<port>/api/values/alamakota/{value}
        [HttpPost("{value:int:max(50)}")]  //routing doklejany do adresu kontrolera
        [HttpPost("alamakota/{value:int:max(50)}")] //metoda dostępna pod dodatkowym adresem /api/[controller]/alamakota/...
        public void Post(int value)
        {
            _values.Add(value);
        }

        //PUT: localhost:<port>/alamakota/{oldValue}/{newValue}
        // ukośnik "/" na początku routingu powoduje, że nie doklejamy do adresu kontrolera, ale tworzymy nowy adres od roota
        [HttpPut("/alamakota/{oldValue:int}/{newValue:int}")] // nazwy w ścieżkach muszą być zgodne z nazwami parametrów metody
        public void Put(int oldValue, int newValue)
        {
            _values[_values.IndexOf(oldValue)] = newValue;
        }

        [HttpDelete("{value:int}")]
        public void Delete(int value)
        {
            _values.Remove(value);
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
        }
    }
}

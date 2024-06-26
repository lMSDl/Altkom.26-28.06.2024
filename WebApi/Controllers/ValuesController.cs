using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")] //asres naszego kontrolera - w nawiasach kwadratowych nazwa klasy bez "Controller"
    [ApiController] //oznaczamy nasz kontroler jako API
    public class ValuesController : ControllerBase
    {
        IList<int> _values;
        public ValuesController(IList<int> values)
        {
            _values = values;
        }

        [HttpGet] //oznaczenie metody GET - metoda nie musi nosić w nazwie lub nazywać się "Get"
        public IEnumerable<int> AlaMaKota()
        {
            return _values;
        }

        [HttpPost("{value:int:max(50)}")]
        public void Post(int value)
        {
            _values.Add(value);
        }

        [HttpPut("{oldValue:int}/{newValue:int}")] // nazwy w ścieżkach muszą być zgodne z nazwami parametrów metody
        public void Put(int oldValue, int newValue)
        {
            _values[_values.IndexOf(oldValue)] = newValue;
        }

        [HttpDelete("{value:int}")]
        public void Delete(int value)
        {
            _values.Remove(value);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    //adnotacje są dziedziczone
    [Route("api/[controller]")] //asres naszego kontrolera - w nawiasach kwadratowych nazwa klasy bez "Controller"
    [ApiController] //oznaczamy nasz kontroler jako API
    public abstract class ApiController : ControllerBase
    {
    }
}

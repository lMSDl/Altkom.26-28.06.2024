using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    public abstract class RUDController<T> : ApiController where T : Entity
    {
        private IRUDService<T> _service;

        protected RUDController(IRUDService<T> service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var item = _service.ReadAsync(id);
            if (item == null)
                return NotFound();

            await Task.Delay(1000, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            await Task.Delay(500);

            return Ok(item);
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

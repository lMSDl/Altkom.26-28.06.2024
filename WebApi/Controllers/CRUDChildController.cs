using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    public abstract class CRUDChildController<T, TParent> : RUDController<T> where T : Entity
    {
        private ICRUDChildService<T> _service;
        private IRUDService<TParent> _parentService;

        protected CRUDChildController(ICRUDChildService<T> service, IRUDService<TParent> parentService) : base(service)
        {
            _service = service;
            _parentService = parentService;
        }

        public virtual async Task<IActionResult> GetAll(int parentId)
        {
            if (await _parentService.ReadAsync(parentId) == null)
                return NotFound();

            return Ok(await _service.ReadAllAsync(parentId));
        }

        public virtual async Task<IActionResult>Post(int parentId, T item)
        {
            if (await _parentService.ReadAsync(parentId) == null)
                return NotFound();

            item = await _service.CreateAsync(parentId, item);

            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
    }
}

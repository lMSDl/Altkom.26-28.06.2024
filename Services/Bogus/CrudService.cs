using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class CRUDService<T>(EntityFaker<T> faker) : BaseCRUDService<T>(faker), ICRUDService<T> where T : Entity
    {
        public Task<IEnumerable<T>> ReadAllAsync()
        {
            return Task.FromResult(Entities.ToList().AsEnumerable());
        }

        Task<T> ICRUDService<T>.CreateAsync(T entity)
        {
            return CreateAsync(entity);
        }
    }
}

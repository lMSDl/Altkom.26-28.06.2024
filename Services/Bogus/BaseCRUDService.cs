using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public abstract class BaseCRUDService<T>(EntityFaker<T> faker) : RUDService<T>(faker) where T : Entity
    {
        protected Task<T> CreateAsync(T entity)
        {
            int id = Entities.Select(x => x.Id).DefaultIfEmpty(0).Max() + 1;

            entity.Id = id;
            Entities.Add(entity);

            return Task.FromResult(entity);
        }
    }
}

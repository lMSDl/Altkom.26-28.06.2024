using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class CrudService<T> : ICrudService<T> where T : Entity
    {
        protected ICollection<T> Entities { get; }

        public CrudService(EntityFaker<T> faker)
        {
            Entities = faker.Generate(50);
        }

        public Task<T> CreateAsync(T entity)
        {
            int id = Entities.Select(x => x.Id).DefaultIfEmpty(0).Max() + 1;

            entity.Id = id;
            Entities.Add(entity);

            return Task.FromResult(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await ReadAsync(id);
            Entities.Remove(entity);
        }

        public Task<IEnumerable<T>> ReadAsync()
        {
            return Task.FromResult(Entities.ToList().AsEnumerable());
        }

        public Task<T?> ReadAsync(int id)
        {
            return Task.FromResult(Entities.SingleOrDefault(x => x.Id == id));
        }

        public async Task UpdateAsync(int id, T entity)
        {
            var localEntity = await ReadAsync(id);
            Entities.Remove(localEntity);
            entity.Id = id;
            Entities.Add(entity);
        }
    }
}

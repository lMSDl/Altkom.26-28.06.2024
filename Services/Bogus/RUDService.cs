using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class RUDService<T>(EntityFaker<T> faker) : IRUDService<T> where T : Entity
    {
        protected ICollection<T> Entities { get; } = faker.Generate(50);

        public async Task DeleteAsync(int id)
        {
            var entity = await ReadAsync(id);
            Entities.Remove(entity);
        }

        public Task<T?> ReadAsync(int id)
        {
            return Task.FromResult(Entities.SingleOrDefault(x => x.Id == id));
        }

        public async Task UpdateAsync(int id, T entity)
        {
            await DeleteAsync(id);
            entity.Id = id;
            Entities.Add(entity);
        }
    }
}

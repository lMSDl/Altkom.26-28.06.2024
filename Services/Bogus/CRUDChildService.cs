using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus
{
    public class CRUDChildService<T>(EntityFaker<T> faker) : BaseCRUDService<T>(faker), ICRUDChildService<T> where T : ChildEntity
    {
        public Task<T> CreateAsync(int parentId, T entity)
        {
            entity.ParentId = parentId;
            return CreateAsync(entity);
        }

        public Task<IEnumerable<T>> ReadAllAsync(int parentId)
        {
            return Task.FromResult(Entities.Where(x => x.ParentId == parentId).ToList().AsEnumerable());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICRUDChildService<T> : IRUDService<T>
    {
        Task<IEnumerable<T>> ReadAllAsync(int parentId);
        Task<T> CreateAsync(int parentId, T entity);
    }
}

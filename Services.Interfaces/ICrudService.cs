﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICRUDService<T> : IRUDService<T>
    {
        Task<IEnumerable<T>> ReadAllAsync();
        Task<T> CreateAsync(T entity);
    }
}

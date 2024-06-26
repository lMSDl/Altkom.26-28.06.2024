using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPeopleService : ICrudService<Person>
    {
        Task<IEnumerable<Person>> SearchByFirstName(string firstName);
    }
}

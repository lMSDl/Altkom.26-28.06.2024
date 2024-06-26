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
    public class PeopleService : CrudService<Person>, IPeopleService
    {

        public PeopleService(EntityFaker<Person> faker) : base(faker)
        {
        }

        public Task<IEnumerable<Person>> SearchByFirstName(string firstName)
        {
           return Task.FromResult( Entities.Where(x => string.Compare(x.FirstName, firstName, true) == 0).ToList().AsEnumerable());
        }
    }
}

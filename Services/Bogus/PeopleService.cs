using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class PeopleService(EntityFaker<Person> faker) : CRUDService<Person>(faker), IPeopleService
    {
        public Task<IEnumerable<Person>> SearchByFirstName(string firstName)
        {
            return Task.FromResult(Entities.Where(x => string.Compare(x.FirstName, firstName, true) == 0).ToList().AsEnumerable());
        }
    }
}

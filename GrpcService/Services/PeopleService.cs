using Grpc.Core;
using GrpcService.Protos;
using Services.Interfaces;

namespace GrpcService.Services
{
    public class PeopleService(IPeopleService service) : GrpcService.Protos.PeopleService.PeopleServiceBase
    {


        public override async Task<People> Read(Protos.Void request, ServerCallContext context)
        {
            var people = await service.ReadAllAsync();

            var response = new People();
            response.Collection.AddRange(people.Select(x => new Person { FirstName = x.FirstName, Id = x.Id, LastName = x.LastName }));


            return response;
        }

        public override async Task<Person> ReadById(Id request, ServerCallContext context)
        {
            var person = await service.ReadAsync(request.Value);
            if (person == null)
                return new Person();


            var response = new Person { FirstName = person.FirstName, LastName = person.LastName, Id = person.Id };

            return response;
        }

        public override async  Task<Protos.Void> Delete(Id request, ServerCallContext context)
        {
            await service.DeleteAsync(request.Value);

            return new Protos.Void();
        }
    }
}

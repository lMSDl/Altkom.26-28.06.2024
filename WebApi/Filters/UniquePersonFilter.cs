using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Interfaces;

namespace WebApi.Filters
{
    public class UniquePersonFilter : IAsyncActionFilter
    {
        private IPeopleService _peopleService;

        public UniquePersonFilter(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var entity = context.ActionArguments["item"] as Person;

            var duplicate = (await _peopleService.SearchByFirstName(entity.FirstName)).Any(x => x.LastName == entity.LastName);

            if(duplicate)
            {
                context.ModelState.AddModelError(nameof(Models.Person), "First and last name must be unique");
            }

            if (!context.ModelState.IsValid) {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            await next();
        }
    }
}

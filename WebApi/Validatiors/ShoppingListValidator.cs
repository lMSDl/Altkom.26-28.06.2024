using FluentValidation;
using Models;
using Services.Interfaces;

namespace WebApi.Validatiors
{
    public class ShoppingListValidator : AbstractValidator<ShoppingList>
    {
        public ShoppingListValidator(ICRUDService<ShoppingList> service)
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("puste??")
                .Length(5, 15)
                .Must(x => x.EndsWith(".")).WithMessage("Wartość musi kończyć się kropką!")
                .WithName("Nazwa listy");


            RuleFor(x => x.Name).Must(x => !service.ReadAllAsync().Result.Any(xx => xx.Name == x)).WithMessage("Lista o tej nazwie już istnieje");

        }
    }
}

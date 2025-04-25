using FluentValidation;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Validators
{
    public class IngredientUpdateValidator : AbstractValidator<IngredientUpdateDto>
    {
        public IngredientUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
            RuleFor(x => x.UnitMeasureId)
                .NotEmpty().WithMessage("UnitMeasureId is required.")
                .GreaterThan(0).WithMessage("UnitMeasureId must be greater than 0.");
        }
    }
}

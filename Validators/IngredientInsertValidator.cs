using FluentValidation;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Validators
{
    public class IngredientInsertValidator : AbstractValidator<IngredientInsertDto>
    {
        public IngredientInsertValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
            RuleFor(x => x.UnitMeasureId)
                .GreaterThan(0).WithMessage("UnitMeasureId must be greater than 0.");
        }
    }
}

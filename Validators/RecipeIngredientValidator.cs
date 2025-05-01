using FluentValidation;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Validators
{
    public class RecipeIngredientInsertValidator : AbstractValidator<RecipeIngredientInsertDto>
    {
        public RecipeIngredientInsertValidator()
        {
            RuleFor(x => x.IngredientId)
                .NotEmpty().WithMessage("IngredientId is required.")
                .GreaterThan(0).WithMessage("IngredientId must be greater than 0.");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }   
}

using FluentValidation;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Validators
{
    public class RecipeInstanceUpdateValidator : AbstractValidator<RecipeInstanceUpdateDto>
    {
        public RecipeInstanceUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor((x => x.RecipeId))
                .NotEmpty().WithMessage("RecipeId is required.")
                .GreaterThan(0).WithMessage("RecipeId must be greater than 0.");
            RuleFor(x => x.TotalServings)
                .NotEmpty().WithMessage("TotalServings is required.")
                .GreaterThan(0).WithMessage("TotalServings must be greater than 0.");
            RuleFor(x => x.Notes)
                .NotEmpty().WithMessage("Notes should not be empty.")
                .When(x => !string.IsNullOrEmpty(x.Notes));
        }
    }
}
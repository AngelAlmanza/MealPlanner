using FluentValidation;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Validators
{
    public class RecipeUpdateValidator : AbstractValidator<RecipeUpdateDto>
    {
        public RecipeUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 255).WithMessage("Name must be between 2 and 255 characters.");
            RuleFor(x => x.Instructions)
                .NotEmpty().WithMessage("Instructions is required.")
                .Length(2, 1000).WithMessage("Instructions must be between 2 and 1000 characters.");
            RuleFor(x => x.Servings)
                .NotEmpty().WithMessage("Servings is required.")
                .GreaterThan(0).WithMessage("Servings must be greater than 0.");
            RuleFor(x => x.Url)
                .Length(0, 1000).WithMessage("Url must be less than 1000 characters.")
                .Matches(@"^(http|https)://").WithMessage("Url must start with http:// or https://")
                .When(x => !string.IsNullOrEmpty(x.Url));
        }
    }
}
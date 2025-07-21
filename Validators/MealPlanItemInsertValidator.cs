using FluentValidation;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Validators
{
    public class MealPlanItemInsertValidator : AbstractValidator<MealPlanItemInsertDto>
    {
        public MealPlanItemInsertValidator()
        {
            RuleFor(x => x.Date).NotEmpty()
                .NotEmpty().WithMessage("Date is required.")
                .GreaterThan(DateTime.Now).WithMessage("Date must be greater than today.");
            RuleFor(x => x.MealType)
                .IsInEnum().WithMessage("MealType must be a valid enum value.");
            RuleFor(x => x.RecipeId)
                .NotEmpty().WithMessage("RecipeId is required.")
                .GreaterThan(0).WithMessage("RecipeId must be greater than 0.");
        }
    }
}
using FluentValidation;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Validators
{
    public class MealPlanEntryUpdateValidator : AbstractValidator<MealPlanEntryUpdateDto>
    {
        public MealPlanEntryUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("MealPlanEntryUpdateDto.Id is required.")
                .GreaterThan(0).WithMessage("MealPlanEntryUpdateDto.Id must be greater than 0.");
            RuleFor(x => x.MealPlanWeekId)
                .NotEmpty().WithMessage("MealPlanWeekId is required.")
                .GreaterThan(0).WithMessage("MealPlanWeekId must be greater than 0.");
            RuleFor(x => x.RecipeInstanceId)
                .NotEmpty().WithMessage("RecipeInstanceId is required.")
                .GreaterThan(0).WithMessage("RecipeInstanceId must be greater than 0.");
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.")
                .GreaterThan(DateTime.MinValue).WithMessage("Date must be a valid date.");
            RuleFor(x => x.ServingsUsed)
                .NotEmpty().WithMessage("ServingsUsed is required.")
                .GreaterThan(0).WithMessage("ServingsUsed must be greater than 0.");
            RuleFor(x => x.MealType)
                .NotEmpty().WithMessage("MealType is required.")
                .IsInEnum().WithMessage("MealType must be a valid enum value.");
        }
    
    }
}
using FluentValidation;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Validators
{
    public class MealPlanWeekUpdateValidator : AbstractValidator<MealPlanWeekUpdateDto>
    {
        public MealPlanWeekUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("StartDate is required.")
                .GreaterThan(DateTime.MinValue).WithMessage("StartDate must be a valid date.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("StartDate cannot be in the future.");
            RuleFor(x => x.EndDate)
                .NotNull().WithMessage("EndDate is required.")
                .GreaterThan(DateTime.MinValue).WithMessage("EndDate must be a valid date.")
                .Must((dto, endDate) => endDate > dto.StartDate && (endDate - dto.StartDate).TotalHours >= 1)
                .WithMessage("EndDate must be greater than StartDate and at least 1 hour apart.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name));
        }
    }
}
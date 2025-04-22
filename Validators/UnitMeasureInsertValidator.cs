using FluentValidation;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Validators
{
    public class UnitMeasureInsertValidator : AbstractValidator<UnitMeasureInsertDto>
    {
        public UnitMeasureInsertValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
            RuleFor(x => x.Abbreviation)
                .NotEmpty().WithMessage("Abbreviation is required.")
                .Length(1, 10).WithMessage("Abbreviation must be between 1 and 10 characters.");
        }
    }
}

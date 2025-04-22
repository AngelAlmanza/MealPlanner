using FluentValidation;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Validators
{
    public class UnitMeasureUpdateValidator : AbstractValidator<UnitMeasureUpdateDto>
    {
        public UnitMeasureUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
            RuleFor(x => x.Abbreviation)
                .NotEmpty().WithMessage("Abbreviation is required.")
                .Length(1, 10).WithMessage("Abbreviation must be between 1 and 10 characters.");
        }
    }
}

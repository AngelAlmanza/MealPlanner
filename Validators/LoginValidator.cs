using FluentValidation;
using MealPlannerApi.DTOs;

namespace MealPlannerApi.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
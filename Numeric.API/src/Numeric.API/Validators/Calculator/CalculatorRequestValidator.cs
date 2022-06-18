using Calculator.Models;
using FluentValidation;
using Numeric.API.Models.Calculator;
using System.Text.RegularExpressions;

namespace Numeric.API.Validators.Calculator
{
    public class CalculatorRequestValidator : AbstractValidator<CalculatorRequest>
    {
        public CalculatorRequestValidator()
        {
            var allowedCharacters = string.Join(null, Characters.Numbers)
                + string.Join(null, Characters.Operators)
                + string.Join(null, Characters.Parentheses)
                + string.Join(null, Characters.Points);

            string pattern = $"^[{allowedCharacters}]+$";

            RuleFor(x => x.Expression)
                .NotEmpty();
            When(x => x.Expression != null, () =>
            {
                RuleFor(x => x.Expression)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .Must(x => x.Length > 2 && x.Length <= 500)
                    .WithMessage("Length of expression shoul be between 3 and 500")
                    .Must(x => Regex.IsMatch(Regex.Replace(x, @"\s+", ""), pattern))
                    .WithMessage("Incompatible characters used");
            });
        }
    }
}

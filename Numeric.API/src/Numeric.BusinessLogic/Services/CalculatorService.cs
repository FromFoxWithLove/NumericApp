using Calculator.Abstractions;
using Calculator.Exceptions;
using Numeric.BusinessLogic.Extensions;
using Numeric.BusinessLogic.Interfaces;
using Numeric.BusinessLogic.Models;

namespace Numeric.BusinessLogic.Services
{
    public class CalculatorService : ICalculatorService
    {
        public const string ValidateError = "Invalid expression";
        public const string NegativeNumberToFractionalPowerError = "Impossible to raise a negative number to a fractional power";
        public const string DevideByZeroError = "Impossible to devide a number by zero";

        private readonly ICalculator _calculator;
        private readonly IValidator _validator;

        public CalculatorService(ICalculator calculator,
            IValidator validator)
        {
            _calculator = calculator;
            _validator = validator;
        }

        public ValueServiceResult<string> Calculate(string expression)
        {
            var serviceResult = new ValueServiceResult<string>();

            try
            {
                var isValid = _validator.Validate(expression);

                if (isValid)
                {
                    var result = _calculator.Calculate(expression);
                    
                    if (double.IsInfinity(result))
                    {
                        serviceResult.WithValue("Infinity");
                    }
                    else
                    {
                        serviceResult.WithValue(result.ToString());
                    }
                }
                else
                {
                    serviceResult.WithBusinessError(ValidateError);
                }

                return serviceResult;
            }
            catch (NegativeNumberToFractionalPowerException ex)
            {
                serviceResult.WithBusinessError(NegativeNumberToFractionalPowerError);
            }
            catch (DivideByZeroException ex)
            {
                serviceResult.WithBusinessError(DevideByZeroError);
            }
            catch (Exception ex)
            {
                serviceResult.WithException();
            }

            return serviceResult;
        }
    }
}

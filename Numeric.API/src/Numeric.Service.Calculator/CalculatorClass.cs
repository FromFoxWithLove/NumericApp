using Calculator.Abstractions;
using Calculator.Enums;
using Calculator.Exceptions;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorClass : ICalculator
    {
        private readonly IValidator _validator;
        private readonly IExpressionParser _parser;

        public CalculatorClass(IValidator validator, 
            IExpressionParser parser)
        {
            _validator = validator;
            _parser = parser;
        }

        public double Calculate(string expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            expression = expression.Replace(" ", "");

            if (!_validator.Validate(expression))
            {
                throw new ArgumentException(nameof(expression));
            }

            var openParenthesPosition = -1;
            var firstCloseParenthesPosition = -1;

            bool haveParentheses = true;
            var result = 0d;

            while (haveParentheses)
            {
                haveParentheses = false;

                for (var i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '(')
                    {
                        openParenthesPosition = i;
                        haveParentheses = true;
                    }

                    if (expression[i] == ')')
                    {
                        firstCloseParenthesPosition = i;
                        break;
                    }
                }

                if (haveParentheses)
                {
                    var simpleExpression = expression.Substring(openParenthesPosition + 1, firstCloseParenthesPosition - openParenthesPosition - 1);

                    var intermediateResult = CalculateSimpleExpresion(simpleExpression);

                    expression = expression.Substring(0, openParenthesPosition) + intermediateResult + expression.Substring(firstCloseParenthesPosition + 1);
                }
                else
                {
                    result = CalculateSimpleExpresion(expression);
                }
            }

            return result;
        }

        public double CalculateSimpleExpresion(string expresion)
        {
            _parser.ParseSimpleExpression(expresion,out var numberPositions, out var operatorPositions);

            CountExponentiations(ref numberPositions, ref operatorPositions);

            CountMultiplicationsAndDivisions(ref numberPositions, ref operatorPositions);

            CountAdditionsAndSubstractions(ref numberPositions, ref operatorPositions);

            return numberPositions.FirstOrDefault().Value;
        }
        
        public void CountExponentiations(ref Dictionary<int, double> numberPositions, ref Dictionary<int, char> operatorPositions)
        {
            while (operatorPositions.Where(x => x.Value == '^').Count() > 0)
            {
                var powerPosition = operatorPositions.Where(x => x.Value == '^')
                    .OrderBy(x => x.Key)
                    .LastOrDefault();

                var number = numberPositions.Where(x => x.Key == powerPosition.Key).FirstOrDefault();
                var powerNumber = numberPositions.Where(x => x.Key == powerPosition.Key + 1).FirstOrDefault();

                if (number.Value < 0 && powerNumber.Value % 1 != 0)
                {
                    throw new NegativeNumberToFractionalPowerException();
                }

                var result = Math.Pow(number.Value, powerNumber.Value);

                WriteResultToDictionaries(ref numberPositions, ref operatorPositions, powerPosition.Key, number.Key, result);
            }
        }

        public void CountMultiplicationsAndDivisions(ref Dictionary<int, double> numberPositions, ref Dictionary<int, char> operatorPositions)
        {
            while (operatorPositions.Where(x => x.Value == '*' || x.Value == '/').Count() > 0)
            {
                var operatorPosition = operatorPositions.Where(x => x.Value == '*' || x.Value == '/')
                    .OrderBy(x => x.Value)
                    .LastOrDefault();

                var firstNumber = numberPositions.Where(x => x.Key == operatorPosition.Key).FirstOrDefault();
                var secondNumber = numberPositions.Where(x => x.Key == operatorPosition.Key + 1).FirstOrDefault();

                double result;

                if (operatorPosition.Value == '*')
                {
                    result = firstNumber.Value * secondNumber.Value;
                }
                else
                {
                    if (secondNumber.Value == 0)
                    {
                        throw new DivideByZeroException();
                    }

                    result = firstNumber.Value / secondNumber.Value;
                }

                WriteResultToDictionaries(ref numberPositions, ref operatorPositions, operatorPosition.Key, firstNumber.Key, result);
            }
        }

        public void CountAdditionsAndSubstractions(ref Dictionary<int, double> numberPositions, ref Dictionary<int, char> operatorPositions)
        {
            while (operatorPositions.Where(x => x.Value == '+' || x.Value == '-').Count() > 0)
            {
                var operatorPosition = operatorPositions.Where(x => x.Value == '+' || x.Value == '-')
                    .OrderBy(x => x.Value)
                    .LastOrDefault();

                var firstNumber = numberPositions.Where(x => x.Key == operatorPosition.Key).FirstOrDefault();
                var secondNumber = numberPositions.Where(x => x.Key == operatorPosition.Key + 1).FirstOrDefault();

                double result;

                if (operatorPosition.Value == '+')
                {
                    result = firstNumber.Value + secondNumber.Value;
                }
                else
                {
                    result = firstNumber.Value - secondNumber.Value;
                }

                WriteResultToDictionaries(ref numberPositions, ref operatorPositions, operatorPosition.Key, firstNumber.Key, result);
            }
        }

        private void WriteResultToDictionaries(ref Dictionary<int, double> numberPositions, ref Dictionary<int, char> operatorPositions, int operatorPosition, int numberPosition, double result)
        {
            numberPositions.Remove(operatorPosition);
            numberPositions.Remove(operatorPosition + 1);
            operatorPositions.Remove(operatorPosition);

            var newNumberPositions = numberPositions.Select(numberItem =>
            {
                if (numberItem.Key <= numberPosition)
                {
                    return numberItem;
                }
                else
                {
                    return new KeyValuePair<int, double>(numberItem.Key - 1, numberItem.Value);
                }
            });

            numberPositions = newNumberPositions.ToDictionary(x => x.Key, x => x.Value);
            numberPositions.Add(numberPosition, result);

            var newOperatorPositions = operatorPositions.Select(operatorItem =>
            {
                if (operatorItem.Key <= operatorPosition)
                {
                    return operatorItem;
                }
                else
                {
                    return new KeyValuePair<int, char>(operatorItem.Key - 1, operatorItem.Value);
                }
            });

            operatorPositions = newOperatorPositions.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}

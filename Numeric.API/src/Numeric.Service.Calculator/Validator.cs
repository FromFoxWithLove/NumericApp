using Calculator.Abstractions;
using Calculator.Enums;
using Calculator.Helpers;
using Calculator.Models;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Validator : IValidator
    {
        public bool Validate(string expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var allowedCharacters = string.Join(null, Characters.Numbers)
                + string.Join(null, Characters.Operators)
                + string.Join(null, Characters.Parentheses)
                + string.Join(null, Characters.Points);

            string pattern = $"^[{allowedCharacters}]+$";
            var isMatch = Regex.IsMatch(expression, pattern);

            if (isMatch)
            {
                var firstElement = expression[0];

                if (Characters.Operators.Contains(firstElement) && firstElement != '-' || firstElement == ')' || Characters.Points.Contains(firstElement))
                {
                    isMatch = false;
                }

                var lastElement = expression[^1];

                if (Characters.Operators.Contains(lastElement) || lastElement == '(' || Characters.Points.Contains(lastElement))
                {
                    isMatch = false;
                }

                var openBraces = 0;
                var closeBraces = 0;
                var prevElement = ' ';
                var prevElementsType = ElementsTypeEnum.None;

                foreach (var element in expression)
                {
                    var elementsType = ElementsHelper.GetElementsType(element);

                    switch (elementsType)
                    {
                        case ElementsTypeEnum.Parentheses:
                            {
                                if (prevElementsType == ElementsTypeEnum.Point)
                                {
                                    isMatch = false;
                                }

                                if (element == '(')
                                {
                                    if (prevElement == ')' || prevElementsType == ElementsTypeEnum.Number)
                                    {
                                        isMatch = false;
                                    }

                                    openBraces++;
                                }

                                if (element == ')')
                                {
                                    if (prevElementsType == ElementsTypeEnum.Operator || prevElement == '(')
                                    {
                                        isMatch = false;
                                    }

                                    closeBraces++;
                                }

                                break;
                            }

                        case ElementsTypeEnum.Operator:
                            {
                                if (prevElementsType == ElementsTypeEnum.Operator || prevElementsType == ElementsTypeEnum.Point || (prevElement == '(' && element != '-'))
                                {
                                    isMatch = false;
                                }

                                if (element == '-' && prevElement == '^')
                                {
                                    isMatch = true;
                                }

                                break;
                            }

                        case ElementsTypeEnum.Number:
                            {
                                if (prevElement == ')')
                                {
                                    isMatch = false;
                                }

                                break;
                            }

                        case ElementsTypeEnum.Point:
                            {
                                if (prevElementsType != ElementsTypeEnum.Number)
                                {
                                    isMatch = false;
                                }

                                break;
                            }

                    }

                    prevElement = element;
                    prevElementsType = elementsType;

                    if (!isMatch)
                    {
                        break;
                    }
                }

                if (openBraces != closeBraces)
                {
                    isMatch = false;
                }
            }

            return isMatch;
        }
    }
}

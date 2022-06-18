using Calculator.Abstractions;
using Calculator.Enums;
using Calculator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ExpressionParser : IExpressionParser
    {
        public void ParseSimpleExpression(string simpleExpression, out Dictionary<int, double> numberPositions, out Dictionary<int, char> operatorPositions)
        {
            numberPositions = new Dictionary<int, double>();
            operatorPositions = new Dictionary<int, char>();

            var currentNumber = 0d;
            var prevElementsType = ElementsTypeEnum.None;

            var isAfterPoint = false;
            var positionAfterPoint = 1;

            var position = 0;

            var isNegativeNumber = false;

            foreach (var element in simpleExpression)
            {
                var elementsType = ElementsHelper.GetElementsType(element);

                switch (elementsType)
                {
                    case ElementsTypeEnum.Number:
                        {
                            if (prevElementsType == ElementsTypeEnum.Operator)
                            {
                                currentNumber = 0;
                            }

                            var currentDigit = int.Parse(element.ToString());
                            if (isAfterPoint)
                            {
                                positionAfterPoint *= 10;

                                currentNumber = currentNumber + (double)currentDigit / (positionAfterPoint);
                            }
                            else
                            {
                                currentNumber = currentNumber * 10 + currentDigit;
                            }

                            break;
                        }
                    case ElementsTypeEnum.Operator:
                        {
                            if (prevElementsType == ElementsTypeEnum.Operator || prevElementsType == ElementsTypeEnum.None)
                            {
                                isNegativeNumber = true;
                            }
                            else
                            {
                                position++;

                                if (isNegativeNumber)
                                {
                                    currentNumber = 0 - currentNumber;
                                    isNegativeNumber = false;
                                }

                                numberPositions.Add(position, currentNumber);
                                operatorPositions.Add(position, element);
                            }

                            isAfterPoint = false;
                            positionAfterPoint = 1;
                            break;
                        }
                    case ElementsTypeEnum.Point:
                        {
                            isAfterPoint = true;
                            break;
                        }

                }
                prevElementsType = elementsType;
            }

            position++;
            if (isNegativeNumber)
            {
                currentNumber = 0 - currentNumber;
            }

            numberPositions.Add(position, currentNumber);
        }
    }
}

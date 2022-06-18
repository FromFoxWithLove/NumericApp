using Calculator.Enums;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Helpers
{
    public class ElementsHelper
    {
        public static ElementsTypeEnum GetElementsType(char element)
        {
            if (Characters.Numbers.Contains(element))
            {
                return ElementsTypeEnum.Number;
            }

            if (Characters.Operators.Contains(element))
            {
                return ElementsTypeEnum.Operator;
            }

            if (Characters.Parentheses.Contains(element))
            {
                return ElementsTypeEnum.Parentheses;
            }

            if (Characters.Points.Contains(element))
            {
                return ElementsTypeEnum.Point;
            }

            throw new ArgumentException();
        }
    }
}

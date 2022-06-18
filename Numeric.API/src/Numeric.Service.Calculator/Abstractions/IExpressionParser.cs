using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Abstractions
{
    public interface IExpressionParser
    {
        void ParseSimpleExpression(string simpleExpression, out Dictionary<int, double> numberPositions, out Dictionary<int, char> operatorPositions);
    }
}

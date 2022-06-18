using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Abstractions
{
    public interface ICalculator
    {
        double Calculate(string expression);
    }
}

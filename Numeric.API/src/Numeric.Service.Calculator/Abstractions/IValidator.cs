using Calculator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Abstractions
{
    public interface IValidator
    {
        bool Validate(string expression);
    }
}

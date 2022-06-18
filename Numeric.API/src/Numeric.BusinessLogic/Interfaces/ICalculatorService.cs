using Numeric.BusinessLogic.Models;

namespace Numeric.BusinessLogic.Interfaces
{
    public interface ICalculatorService
    {
        ValueServiceResult<string> Calculate(string expression);
    }
}

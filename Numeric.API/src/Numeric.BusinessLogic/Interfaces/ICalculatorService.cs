using Numeric.BusinessLogic.Models;

namespace Numeric.BusinessLogic.Interfaces
{
    public interface ICalculatorService
    {
        ValueServiceResult<double> Calculate(string expression);
    }
}

using Numeric.BusinessLogic.Enums;

namespace Numeric.BusinessLogic.Models
{
    public class ErrorModel
    {
        public ErrorType Type { get; set; }

        public string? Message { get; set; }
    }
}

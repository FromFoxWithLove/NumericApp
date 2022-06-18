namespace Numeric.BusinessLogic.Models
{
    public class ValueServiceResult<T> : ServiceResult
    {
        public T? Value { get; set; }

        public bool IsEmpty => Value is null;
    }
}

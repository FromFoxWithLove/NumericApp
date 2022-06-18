using Numeric.BusinessLogic.Enums;
using Numeric.BusinessLogic.Models;

namespace Numeric.BusinessLogic.Extensions
{
    public static class ServiceResultExtensions
    {
        public const string DefaultExceptionMessage = "An unhandled exception occurred.";

        public static TResult WithValue<TResult, TValue>(this TResult serviceResult, TValue value) where TResult : ValueServiceResult<TValue>
        {
            if (serviceResult is null)
            {
                throw new ArgumentNullException(nameof(serviceResult));
            }

            serviceResult.Value = value;

            return serviceResult;
        }

        public static T WithBusinessError<T>(this T serviceResult, string message) where T : ServiceResult
        {
            return serviceResult.WithError(ErrorType.BusinessError, message);
        }

        public static T WithException<T>(this T serviceResult, string? message = null) where T : ServiceResult
        {
            return serviceResult.WithError(ErrorType.Exception, message ?? DefaultExceptionMessage);
        }

        public static T WithError<T>(this T serviceResult, ErrorType errorType, string message) where T : ServiceResult
        {
            if (serviceResult is null)
            {
                throw new ArgumentNullException(nameof(serviceResult));
            }

            serviceResult.Error = new ErrorModel
            {
                Message = message,
                Type = errorType
            };

            return serviceResult;
        }
    }
}

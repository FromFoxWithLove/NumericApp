using Calculator;
using Calculator.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Numeric.Service.Calculator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceCalculator(this IServiceCollection services)
        {
            services.AddTransient<ICalculator, CalculatorClass>();
            services.AddTransient<IValidator, Validator>();
            services.AddTransient<IExpressionParser, ExpressionParser>();

            return services;
        }
    }
}

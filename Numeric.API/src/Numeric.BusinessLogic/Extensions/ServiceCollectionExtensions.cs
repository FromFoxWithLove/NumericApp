using Microsoft.Extensions.DependencyInjection;
using Numeric.BusinessLogic.Interfaces;
using Numeric.BusinessLogic.Services;
using Numeric.Service.Calculator.Extensions;

namespace Numeric.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<ICalculatorService, CalculatorService>();
            services.AddServiceCalculator();

            return services;
        }
    }
}

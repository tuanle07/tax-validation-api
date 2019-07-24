using Microsoft.Extensions.DependencyInjection;
using TaxValidationApi.Services.TaxService;
using TaxValidationApi.Services.TaxService.WeightingFactors;

namespace TaxValidationApi.Extensions
{
    public static class ServiceExtentions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IWeightingFactorProviderFactory, WeightingFactorProviderFactory>();
            services.AddScoped<ITaxService, TaxService>();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using TaxValidationApi.Services.TaxService;

namespace TaxValidationApi.Extensions
{
    public static class ServiceExtentions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ITaxService, TaxService>();
        }
    }
}
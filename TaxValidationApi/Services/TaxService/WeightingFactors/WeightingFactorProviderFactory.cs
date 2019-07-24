using Microsoft.AspNetCore.Http;
using TaxValidationApi.Models;

namespace TaxValidationApi.Services.TaxService.WeightingFactors
{
    public class WeightingFactorProviderFactory : IWeightingFactorProviderFactory
    {
        public IWeightingFactor GetWeightingFactorProvider(int totalDigits)
        {
            IWeightingFactor weightingFactorProvider;
            switch (totalDigits)
            {
                case 8:
                    weightingFactorProvider = new WeightingFactors8();
                    break;
                case 9:
                    weightingFactorProvider = new WeightingFactors9();
                    break;
                default:
                    throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError,
                        "No weighting factor provider found");
            }

            return weightingFactorProvider;
        }
    }
}
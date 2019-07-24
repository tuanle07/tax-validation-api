using Microsoft.AspNetCore.Http;
using System.Linq;
using TaxValidationApi.Models;
using TaxValidationApi.Services.TaxService.WeightingFactors;

namespace TaxValidationApi.Services.TaxService
{
    public class TaxService : ITaxService
    {
        private IWeightingFactorProviderFactory _weightingFactorProviderFactory { get; set; }

        public TaxService(IWeightingFactorProviderFactory weightingFactorProviderFactory)
        {
            _weightingFactorProviderFactory = weightingFactorProviderFactory;
        }

        public bool GetTfnValidity(long tfn)
        {
            if (tfn <= 0)
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "TFN cannot be a negative");
            }

            //converting tfn to array of digits
            var digits = tfn.ToString()
                .Select(t => int.Parse(t.ToString()))
                .ToArray();

            if (digits.Length != 8 && digits.Length != 9)
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest,
                    "TFN must be 8 or 9 digits long");
            }

            var weightingFactorProvider = _weightingFactorProviderFactory.GetWeightingFactorProvider(digits.Length);
            var digitsWithWeightingFactor = digits.Select((t, i) => new
            {
                digit = t,
                weighting = weightingFactorProvider.GetWeightingFactor(i)
            });
            var sum = digitsWithWeightingFactor.Aggregate(0, (acc, x) => acc + x.digit * x.weighting);
            return sum % 11 == 0 ? true : false;
        }
    }
}
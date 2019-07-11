using Microsoft.AspNetCore.Http;
using System.Linq;
using TaxValidationApi.Models;
using TaxValidationApi.Services.TaxService.WeightingFactors;

namespace TaxValidationApi.Services.TaxService
{
    public class TaxService : ITaxService
    {
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

            var weightingFactorProvider = GetWeightingFactorProvider(digits.Length);
            var digitsWithWeightingFactor = digits.Select((t, i) => new
            {
                digit = t,
                weighting = weightingFactorProvider.GetWeightingFactor(i)
            });
            var sum = digitsWithWeightingFactor.Aggregate(0, (acc, x) => acc + x.digit * x.weighting);
            return sum % 11 == 0 ? true : false;
        }

        private static IWeightingFactor GetWeightingFactorProvider(int totalDigits)
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
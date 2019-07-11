namespace TaxValidationApi.Services.TaxService.WeightingFactors
{
    public class WeightingFactors8 : IWeightingFactor
    {
        public int GetWeightingFactor(int index)
        {
            switch (index)
            {
                case 0: return 10;
                case 1: return 7;
                case 2: return 8;
                case 3: return 4;
                case 4: return 6;
                case 5: return 3;
                case 6: return 5;
                case 7: return 1;
                default: return 0;
            }
        }
    }
}
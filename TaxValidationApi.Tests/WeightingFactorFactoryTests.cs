using FluentAssertions;
using TaxValidationApi.Services.TaxService.WeightingFactors;
using Xunit;

namespace TaxValidationApi.Tests
{
    public class WeightingFactorFactoryTests
    {
        [Theory]
        [InlineData(8, 1, 7)]
        [InlineData(8, 8, 0)]
        [InlineData(9, 2, 8)]
        [InlineData(9, 8, 1)]
        public void GetWeightingFactor_ReturnCorrectWeightingFactor(int digits, int index, int expectedWeightingFactor)
        {
            // arrange
            var weightingFactorProviderFactory = new WeightingFactorProviderFactory();

            // act
            var weightingFactorProvider = weightingFactorProviderFactory.GetWeightingFactorProvider(digits);
            var weightingFactor = weightingFactorProvider.GetWeightingFactor(index);

            // assert
            weightingFactor.Should().Be(expectedWeightingFactor);
        }
    }
}
using FluentAssertions;
using System;
using TaxValidationApi.Models;
using TaxValidationApi.Services.TaxService;
using Xunit;

namespace TaxValidationApi.Tests
{
    public class TaxServiceTests
    {
        private readonly ITaxService _taxService;

        public TaxServiceTests()
        {
            _taxService = new TaxService();
        }

        [Theory()]
        [InlineData(-123, "TFN cannot be a negative")]
        [InlineData(0, "TFN cannot be a negative")]
        [InlineData(1234567890, "TFN must be 8 or 9 digits long")]
        [InlineData(1234567, "TFN must be 8 or 9 digits long")]
        public void GetTfnValidityWithWrongTfn_ThrowError(long tfn, string errorMessage)
        {
            //act
            Action act = () => _taxService.GetTfnValidity(tfn);

            //assert
            act.Should().Throw<HttpStatusCodeException>().WithMessage(errorMessage);
            //var ex = Assert.Throws<HttpStatusCodeException>(() => _taxService.GetTfnValidity(tfn));
            //Assert.Equal(errorMessage, ex.Message);
        }

        [Theory()]
        [InlineData(714925631)]
        [InlineData(648188480)]
        [InlineData(648188499)]
        [InlineData(648188535)]
        [InlineData(81854402)]
        [InlineData(37118629)]
        [InlineData(85655734)]
        [InlineData(38593503)]
        public void GetTfnValidityWithValidTfn_ReturnTrue(long tfn)
        {
            //act
            var isTfnValid = _taxService.GetTfnValidity(tfn);

            //assert
            isTfnValid.Should().BeTrue();
        }

        [Theory()]
        [InlineData(714925632)]
        [InlineData(64818848)]
        public void GetTfnValidityWithInvalidTfn_ReturnFalse(long tfn)
        {
            //act
            var isTfnValid = _taxService.GetTfnValidity(tfn);

            //assert
            isTfnValid.Should().BeFalse();
        }
    }
}
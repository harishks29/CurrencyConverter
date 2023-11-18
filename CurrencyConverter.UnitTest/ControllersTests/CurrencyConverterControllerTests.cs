using CurrencyConverter.Controllers;
using CurrencyConverter.Models;
using CurrencyConverter.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CurrencyConverter.UnitTests.ControllersTests
{
    internal class CurrencyConverterControllerTests
    {
        private CurrencyConverterController _currencyConverterController;
        private Mock<IExchangeService> _exchangeService;
        private Mock<ILogger<CurrencyConverterController>> _logger;

        [SetUp]
        public void Setup()
        {
            _exchangeService = new Mock<IExchangeService>();
            _logger = new Mock<ILogger<CurrencyConverterController>>();
            _currencyConverterController = new CurrencyConverterController(_exchangeService.Object, _logger.Object);
        }

        [Test]
        public void When_convert_called_with_valid_input_then_return_exchangerate()
        {
            _exchangeService.Setup(x => x.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>())).Returns(74m);
            _exchangeService.Setup(x => x.IsExchangeQueryValid(It.IsAny<ExchangeQuery>())).Returns((true, string.Empty));
            var expected = new JsonResult(new ExchangeResult
            {
                ExchangeRate = 74.0m,
                ConvertedAmount = 740.0m
            });
            var input = new ExchangeQuery { SourceCurrency = "usd", TargetCurrency = "inr", Amount = 10m };

            var result = _currencyConverterController.Convert(input);
            var actual = ((OkObjectResult)result).Value as ExchangeResult;

            Assert.That(actual?.ConvertedAmount, Is.EqualTo(740m));
            Assert.That(actual?.ExchangeRate, Is.EqualTo(74m));
        }

        [Test]
        public void When_convert_called_with_invalid_input_then_return_errormessage()
        {
            _exchangeService.Setup(x => x.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>())).Returns(74m);
            _exchangeService.Setup(x => x.IsExchangeQueryValid(It.IsAny<ExchangeQuery>())).Returns((false, It.IsAny<string>()));
            var expected = new JsonResult(new ExchangeResult
            {
                ExchangeRate = 74.0m,
                ConvertedAmount = 740.0m
            });
            var input = new ExchangeQuery { SourceCurrency = "usd", TargetCurrency = "inr", Amount = 10m };

            var result = _currencyConverterController.Convert(input);
            var actual = ((BadRequestObjectResult)result).StatusCode;

            Assert.That(actual, Is.EqualTo(400));
        }
    }
}

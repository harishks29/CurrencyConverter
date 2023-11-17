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
            var expected = new JsonResult(new ExchangeResult
            {
                ExchangeRate = 74.0m,
                ConvertedAmount = 740.0m
            });

            var result = _currencyConverterController.Convert("usd", "inr", 10.00m);
            var actual = ((JsonResult)result).Value as ExchangeResult;

            Assert.That(actual?.ConvertedAmount, Is.EqualTo(740m));
            Assert.That(actual?.ExchangeRate, Is.EqualTo(74m));
        }
    }
}

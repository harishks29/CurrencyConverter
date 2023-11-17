using CurrencyConverter.Services;
using CurrencyConverter.Services.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CurrencyConverter.UnitTests.ServicesTests
{
    internal class ExchangeServiceTests
    {
        private ExchangeService _exchangeService;
        private Mock<IFileService> _fileService;
        private Mock<ILogger<ExchangeService>> _logger;

        [SetUp]
        public void Setup()
        {
            _fileService = new Mock<IFileService>();
            _fileService.Setup(x => x.LoadExchangeRatesFromJson())
                .ReturnsAsync(new Dictionary<string, decimal> { { "USD_TO_INR", 74M } });
            _logger = new Mock<ILogger<ExchangeService>>();
            _exchangeService = new ExchangeService(_fileService.Object, _logger.Object);
        }

        [Test]
        public void When_getExchangeRate_called_with_valid_input_then_return_exchangerate()
        {         
            var result = _exchangeService.GetExchangeRate("usd", "inr");

            Assert.That(result, Is.EqualTo(74m));
        }
    }
}

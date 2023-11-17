using CurrencyConverter.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CurrencyConverter.UnitTests.ServicesTests
{
    internal class FileServiceTests
    {
        private FileService _fileService;
        private Mock<ILogger<FileService>> _logger;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<FileService>>();
            _fileService = new FileService(_logger.Object);
        }

        [Test]
        public async Task When_loadExchangeRatesFromJson_called_with_valid_input_then_return_exchangerate()
        {
            var str = await _fileService.LoadExchangeRatesFromJson();

            Assert.IsNotNull(str);
        }
    }
}

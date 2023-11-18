using CurrencyConverter.Models;
using CurrencyConverter.Services.Contracts;
using System.Text;

namespace CurrencyConverter.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IFileService _fileService;
        private readonly ILogger<ExchangeService> _logger;
        private Dictionary<string, decimal> _exchangeRates;

        public ExchangeService(IFileService fileService,
                               ILogger<ExchangeService> logger)
        {
            _fileService = fileService;
            _logger = logger;
            LoadExchangeRates();
        }

        private async void LoadExchangeRates()
        {
            _exchangeRates = await _fileService.LoadExchangeRatesFromJson() ?? default;
        }

        public decimal GetExchangeRate(string sourceCurrency, string targetCurrency)
        {
            var exchangeString = sourceCurrency.ToUpper() + "_TO_" + targetCurrency.ToUpper();

            if (_exchangeRates != null && _exchangeRates.Any())
            {
                try
                {
                    return _exchangeRates[exchangeString];
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return 0m;
        }

        public (bool, string) IsExchangeQueryValid(ExchangeQuery exchangeQuery)
        {
            var errorMessage = new StringBuilder();
            var isValid = true;
            if (IsValidCurrencyCode(exchangeQuery.SourceCurrency))
            {
                errorMessage.Append("Source Currency is not valid");
                isValid = false;
            }

            if (IsValidCurrencyCode(exchangeQuery.TargetCurrency))
            {
                if (!isValid)
                {
                    errorMessage.AppendLine();
                }
                errorMessage.Append("Target Currency is not valid");
                isValid = false;
            }

            return (isValid, errorMessage.ToString());
        }

        private bool IsValidCurrencyCode(string currencyCode)
        {
            return !Enum.TryParse(currencyCode, true, out CurrencyCode castedCurrencycode)
                || !Enum.IsDefined(typeof(CurrencyCode), castedCurrencycode);
        }
    }
}

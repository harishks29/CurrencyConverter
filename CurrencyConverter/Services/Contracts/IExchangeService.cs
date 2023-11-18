using CurrencyConverter.Models;

namespace CurrencyConverter.Services.Contracts
{
    public interface IExchangeService
    {
        decimal GetExchangeRate(string sourceCurrency, string targetCurrency);

        (bool, string) IsExchangeQueryValid(ExchangeQuery exchangeQuery);
    }
}

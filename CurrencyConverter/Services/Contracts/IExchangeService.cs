namespace CurrencyConverter.Services.Contracts
{
    public interface IExchangeService
    {
        decimal GetExchangeRate(string sourceCurrency, string targetCurrency);
    }
}

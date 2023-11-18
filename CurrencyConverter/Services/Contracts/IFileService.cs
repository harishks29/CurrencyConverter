namespace CurrencyConverter.Services.Contracts
{
    public interface IFileService
    {
        Task<Dictionary<string, decimal>> LoadExchangeRatesFromJson();
    }
}

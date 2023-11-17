using CurrencyConverter.Services.Contracts;
using System.Text.Json;

namespace CurrencyConverter.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public async Task<Dictionary<string, decimal>?> LoadExchangeRatesFromJson()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\ExchangeRates.json");
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string json = await reader.ReadToEndAsync();
                    return JsonSerializer.Deserialize<Dictionary<string, decimal>>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return default;
            }
        }
    }
}

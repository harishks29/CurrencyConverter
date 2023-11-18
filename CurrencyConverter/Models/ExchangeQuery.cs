namespace CurrencyConverter.Models
{
    public class ExchangeQuery
    {
        public string SourceCurrency { get; set; }

        public string TargetCurrency { get; set; }

        public decimal Amount { get; set; }
    }
}

using CurrencyConverter.Models;
using CurrencyConverter.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace CurrencyConverter.Controllers
{
    [System.Web.Http.Route("api/[controller]")]
    [ApiController]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;
        private readonly ILogger<CurrencyConverterController> _logger;

        public CurrencyConverterController(IExchangeService exchangeService,
            ILogger<CurrencyConverterController> logger)
        {
            _exchangeService = exchangeService;
            _logger = logger;
        }

        [HttpGet("/Convert")]
        public IActionResult Convert([FromQuery] ExchangeQuery exchangeQuery)
        {
            var exchangeQueryIsValidResult = _exchangeService.IsExchangeQueryValid(exchangeQuery);
            if (exchangeQueryIsValidResult.Item1)
            {
                var exchangeRate = _exchangeService.GetExchangeRate(exchangeQuery.SourceCurrency, exchangeQuery.TargetCurrency);
                return Ok(new ExchangeResult
                {
                    ExchangeRate = exchangeRate,
                    ConvertedAmount = exchangeQuery.Amount * exchangeRate
                });
            }
            else
            {
                _logger.LogError(exchangeQueryIsValidResult.Item2);
                return BadRequest(exchangeQueryIsValidResult.Item2);
            }
        }
        
    }
}

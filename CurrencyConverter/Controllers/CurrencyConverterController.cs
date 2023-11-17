using CurrencyConverter.Models;
using CurrencyConverter.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult Convert([Required] string sourceCurrency, [Required] string targetCurrency, [Required] decimal amount)
        {
            var exchangeRate = _exchangeService.GetExchangeRate(sourceCurrency, targetCurrency);
            return new JsonResult(new ExchangeResult
            {
                ExchangeRate = exchangeRate,
                ConvertedAmount = amount * exchangeRate
            });
        }
    }
}

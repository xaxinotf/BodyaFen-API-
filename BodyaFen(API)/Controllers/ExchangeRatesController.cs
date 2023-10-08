using Microsoft.AspNetCore.Mvc;

namespace BodyaFen_API_.Controllers
{
    [Route("/[controller]")]
    public class ExchangeRatesController : Controller
    {
        private readonly BonkRequester _privat24Service;

        public ExchangeRatesController(BonkRequester privat24Service)
        {
            _privat24Service = privat24Service;
        }
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var exchangeRates = await _privat24Service.BonkRequest(token);
            if (exchangeRates.Date == null || exchangeRates.ExchangeRate == null)
                return BadRequest();

            return View(exchangeRates);
            
        }
    }
}

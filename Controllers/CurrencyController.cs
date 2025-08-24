using backend_se.Common.Controllers;
using backend_se.Common.Providers;
using backend_se.Data.Models;
using backend_se.Data.Providers;
using backend_se.Data.Search;
using Microsoft.AspNetCore.Mvc;

namespace backend_se.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : BaseController
    {
        private CurrencyProvider _currencyProivder;

        public CurrencyController(IDataProvider<CurrencyModel, CurrencySearch> currencyProvider)
        {
            _currencyProivder = (CurrencyProvider)currencyProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("Currencies")]
        public IActionResult GetCurrencies()
        {
            var res = _currencyProivder.GetList(new CurrencySearch { });
            return Ok(res);
        }
    }
}

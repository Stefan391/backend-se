using backend_se.Common.Consts;
using backend_se.Common.Controllers;
using backend_se.Common.Providers;
using backend_se.Data.DTO;
using backend_se.Data.Models;
using backend_se.Data.Providers;
using backend_se.Data.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_se.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private ProductProvider _productProvider;
        private CurrencyProvider _currencyProvider;

        public ProductController(IDataProvider<ProductModel, ProductSearch> productProvider, IDataProvider<CurrencyModel, CurrencySearch> currencyProvider)
        {
            _productProvider = (ProductProvider)productProvider;
            _currencyProvider = (CurrencyProvider)currencyProvider;
        }

        [HttpGet("MyProducts")]
        public IActionResult MyProducts()
        {
            var products = _productProvider.GetList(new ProductSearch { UserId = UserId });
            return Ok(products);
        }

        [HttpPost("AllProducts")]
        public IActionResult AllProducts(ProductSearch search)
        {
            search.Displayed = true;
            return Ok(_productProvider.GetList(search));
        }

        [Authorize(Roles = nameof(eUserRole.Basic))]
        [HttpPost("Create")]
        public IActionResult Create(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currency = _currencyProvider.GetById(product.Currency);
            if (currency == null)
                return BadRequest("Invalid Currency");

            if (UserId == null)
                return BadRequest();

            var dbProduct = new ProductModel
            {
                UserId = UserId ?? 0,
                Name = product.Name,
                Description = product.Description,
                Created = DateTime.Now,
                Price = product.Price,
                CurrencyId = currency.Id,
                Condition = (short)product.Condition,
                Displayed = product.Displayed
            };

            _productProvider.Add(dbProduct);

            return Ok("true");
        }
    }
}

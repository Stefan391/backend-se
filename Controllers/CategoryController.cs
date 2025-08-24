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
    public class CategoryController : BaseController
    {
        private ProductProvider _productProvider;
        private CategoryProvider _productCategoryProvider;

        public CategoryController(IDataProvider<ProductModel, ProductSearch> productProvider, IDataProvider<CategoryModel, CategorySearch> productCategoryProvider)
        {
            _productProvider = (ProductProvider)productProvider;
            _productCategoryProvider = (CategoryProvider)productCategoryProvider;
        }

        [HttpGet("Categories")]
        public IActionResult GetCategories()
        {
            return Ok(_productCategoryProvider.GetList(new CategorySearch { }).Select(x => new { value = x.Id, name = x.Name}));
        }
    }
}

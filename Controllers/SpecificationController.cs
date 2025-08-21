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
    public class SpecificationController : BaseController
    {
        private SpecificationProvider _specificationProvider;

        public SpecificationController(IDataProvider<SpecificationModel, SpecificationSearch> specificationProvider)
        {
            _specificationProvider = (SpecificationProvider)specificationProvider;
        }

        [HttpPost("Specifications")]
        public IActionResult Specifications(SpecificationSearch search)
        {
            var specifications = _specificationProvider.GetList(search);
            return Ok(specifications);
        }

        [HttpPost("SearachSpecifications")]
        public IActionResult SearachSpecifications(SpecificationSearch search)
        {
            var specifications = _specificationProvider.GetSearchList(search);
            return Ok(specifications);
        }
    }
}

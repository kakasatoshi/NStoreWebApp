using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Interfaces;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _iProduct;

        public ProductsController(IProduct iProduct)
        {
            _iProduct = iProduct;
        }

        [HttpPost("Paging")]
        public IActionResult GetAllPaging(ProductModel.Input.GetProductPagingRequest request)
        {
            var result = _iProduct.GetAllPaging(request);
            return Ok(result);
        }

        [HttpPost("Detail/{productId}")]
        public IActionResult GetById(int productId)
        {
            var product = _iProduct.GetById(productId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }
    }
}
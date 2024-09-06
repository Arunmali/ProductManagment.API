using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagment.API.Repository;
using ProductManagment.API.Services;

namespace ProductManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetAllProducts")]
        public ActionResult GetAllProducts()
        {
            var response = _productService.GetAll();
            if(response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var response=await _productService.Add(product).ConfigureAwait(false);
            if(!response.IsSuccessStatusCode)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var response = await _productService.Update(product).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var response = await _productService.Delete(productId).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

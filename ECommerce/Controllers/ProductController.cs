using ECommerce.Models.DTO;
using ECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
            => _productService = productService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<ProductDTO>>> GetPagedProducts([FromQuery] BrowseProducts query)
        {
            var result = await _productService.GetPagedAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductDTO>> GetProducById([FromRoute] Guid id)
        {
            var result = await _productService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductById([FromRoute] Guid id)
        {
            await _productService.DeletetAsync(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] AddProduct addProduct)
        {
            var id = await _productService.AddAsync(addProduct);
            return Created($"api/product/{id}", null);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateQuantity([FromBody] UpdateQuantity updateQuantity)
        {
            await _productService.UpdateQuantity(updateQuantity);
            return Ok();
        }
    }
}
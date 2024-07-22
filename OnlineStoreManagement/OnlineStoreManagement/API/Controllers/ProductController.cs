using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreManagement.DTOs;
using OnlineStoreManagement.Services.Interfaces;

namespace OnlineStoreManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //Thêm [Authorize] attribute vào controller để yêu cầu xác thực cho tất cả các endpoint.
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("warmup")]
        public async Task<IActionResult> WarmUp()
        {
            _logger.LogInformation("Warming up the application");
            await _productService.GetProductByIdAsync(0);
            return Ok("Application warmed up successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all products");
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching product with ID {id}", id);
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]

        //Thêm [Authorize(Roles = "Admin")] attribute vào các endpoint tạo,
        //cập nhật và xóa sản phẩm để chỉ cho phép người dùng có vai trò "Admin" truy cập.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            _logger.LogInformation("Creating a new product");
            var created = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, ProductDTO productDto)
        {
            if (id != productDto.Id) return BadRequest();
            _logger.LogInformation("Updating product with ID {id}", id);
            await _productService.UpdateProductAsync(productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting product with ID {id}", id);
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}

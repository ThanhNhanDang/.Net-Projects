using FluentValidation;
using OnlineStoreManagement.DTOs;
using OnlineStoreManagement.Entities;
using OnlineStoreManagement.Exceptions;
using OnlineStoreManagement.Repositories.Interfaces;
using OnlineStoreManagement.Services.Interfaces;
using Serilog;

namespace OnlineStoreManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        //Thêm IValidator<ProductDTO> và ILogger<ProductService>
        //vào constructor để cung cấp validation và logging.

        //Sử dụng _productValidator để thực hiện validation cho các DTO
        //sản phẩm trước khi thực hiện các hoạt động CRUD.
        private readonly IValidator<ProductDTO> _productValidator;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IUnitOfWork unitOfWork, IValidator<ProductDTO> productValidator, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _productValidator = productValidator;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            _logger.LogInformation("Fetching all products");
            var products = await _unitOfWork.Products.GetAllAsync();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                Category = p.Category
            });
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            _logger.LogInformation("Fetching product with ID {id}", id);
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                Category = product.Category
            };
        }

        public async Task<ProductDTO> CreateProductAsync(ProductDTO productDto)
        {
            _logger.LogInformation("Creating a new product");
            await _productValidator.ValidateAndThrowAsync(productDto);

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                Category = productDto.Category
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            productDto.Id = product.Id;
            return productDto;
        }

        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            _logger.LogInformation("Updating product with ID {id}", productDto.Id);
            await _productValidator.ValidateAndThrowAsync(productDto);

            var product = await _unitOfWork.Products.GetByIdAsync(productDto.Id);
            if (product == null) throw new NotFoundException("Product", productDto.Id);

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.StockQuantity = productDto.StockQuantity;
            product.Category = productDto.Category;

            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            _logger.LogInformation("Deleting product with ID {id}", id);
            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}

using AutoMapper;
using EcommerceProductModule.DataContext;
using EcommerceProductModule.Models;
using EcommerceProductModule.Models.Dtos;
using EcommerceProductModule.Models.Dtos.ProductDto;
using EcommerceProductModule.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProductModule.Service
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(AppDbContext context, IMapper mapper)
        {
                _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<ProductResponseDto>> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            try
            {
                var productExists = await _context.Products.FirstOrDefaultAsync(u => u.Name.ToLower() == productCreateDto.Name.ToLower());
                if (productCreateDto != null && productExists == null)
                {
                    var product = _mapper.Map<Product>(productCreateDto);
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();

                    var IsProductAdded = await _context.Products.FirstOrDefaultAsync(u => u.Name.ToLower() == productCreateDto.Name.ToLower());
                    var productResponseDto = _mapper.Map<ProductResponseDto>(IsProductAdded);
                    return new ApiResponse<ProductResponseDto>(200, true, productResponseDto, "Product details added successfully.");
                }
                return new ApiResponse<ProductResponseDto>(400, false, "either product with same name already exists or invalid details.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductResponseDto>(500, false, $"Something went wrong : {ex}");
            }
        }

        public async Task<ApiResponse<ProductResponseDto>> DeleteProductAsync(int ProductID)
        {
            try
            {
                var productExists = await _context.Products.FirstOrDefaultAsync(u => u.Id == ProductID);
                if (productExists != null)
                {
                    _context.Products.Remove(productExists);
                    await _context.SaveChangesAsync();
                    var productResponseDto = _mapper.Map<ProductResponseDto>(productExists);
                    return new ApiResponse<ProductResponseDto>(200, true, productResponseDto, "Product deleted successfully.");
                }
                return new ApiResponse<ProductResponseDto>(400, false, "Product not Found.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductResponseDto>(500, false, $"Something went wrong : {ex}");
            }
        }

        public async Task<ApiResponse<List<ProductResponseDto>>> GetAllProductsByCategoryAsync(int CategoryID)
        {
            var products = await _context.Products.Where(u => u.CategoryId == CategoryID).ToListAsync();
            var productsDtos = _mapper.Map<List<ProductResponseDto>>(products);

            return new ApiResponse<List<ProductResponseDto>>(200, true, productsDtos, $"Product list based on the category.");
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int ProductID)
        {
            try
            {
                var productExists = await _context.Products.FirstOrDefaultAsync(u => u.Id == ProductID);
                if (productExists != null)
                {
                    var productResponseDto = _mapper.Map<ProductResponseDto>(productExists);
                    return productResponseDto;
                    //return new ApiResponse<ProductResponseDto>(200, true, productResponseDto, $"Product found with {ProductID} successfully.");
                }
                return _mapper.Map<ProductResponseDto>(productExists);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<ProductResponseDto>> UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            try
            {
                var productExists = await _context.Products.AsNoTracking().FirstOrDefaultAsync(u => u.Id == productUpdateDto.Id);
                if (productExists != null)
                {
                    productExists = _mapper.Map<Product>(productUpdateDto);
                    _context.Products.Update(productExists);
                    await _context.SaveChangesAsync();
                    var isProductUpdated = await _context.Products.FirstOrDefaultAsync(u => u.Id == productUpdateDto.Id);
                    if (isProductUpdated != null)
                    {
                        var productResponseDto = _mapper.Map<ProductResponseDto>(isProductUpdated);
                        return new ApiResponse<ProductResponseDto>(200, true, productResponseDto, $"Product updated successfully.");
                    }
                }
                return new ApiResponse<ProductResponseDto>(400, false, "Product not found.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductResponseDto>(500, false, $"Something went wrong : {ex}");
            }
        }
    }
}

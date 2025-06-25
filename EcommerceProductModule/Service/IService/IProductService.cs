using EcommerceProductModule.Models.Dtos;
using EcommerceProductModule.Models.Dtos.ProductDto;

namespace EcommerceProductModule.Service.IService
{
    public interface IProductService
    {
        Task<ApiResponse<ProductResponseDto>> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<ApiResponse<ProductResponseDto>> UpdateProductAsync(ProductUpdateDto productUpdateDto);
        Task<ProductResponseDto> GetProductByIdAsync(int ProductID);
        Task<ApiResponse<ProductResponseDto>> DeleteProductAsync(int ProductID);
        Task<ApiResponse<List<ProductResponseDto>>> GetAllProductsByCategoryAsync(int CategoryID);
        // it will implement in future if needed. 
        //Task<ApiResponse<ProductResponseDto>> UpdateProductStatusAsync(ProductStatudUpdateDto productStatudUpdateDto);
    }
}

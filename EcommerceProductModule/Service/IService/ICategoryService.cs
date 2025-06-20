using EcommerceProductModule.Models.Dtos;
using EcommerceProductModule.Models.Dtos.CategoryDto;

namespace EcommerceProductModule.Service.IService
{
    public interface ICategoryService
    {
        Task<ApiResponse<CategoryResponseDto>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<ApiResponse<CategoryResponseDto>> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task<ApiResponse<CategoryResponseDto>> DeleteCategoryAsync(int CategoryID);
        Task<ApiResponse<List<CategoryResponseDto>>> GetAllCategoryAsync();
    }
}

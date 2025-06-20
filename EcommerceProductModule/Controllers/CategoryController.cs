using EcommerceProductModule.Models.Dtos;
using EcommerceProductModule.Models.Dtos.CategoryDto;
using EcommerceProductModule.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace EcommerceProductModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
                _categoryService = categoryService;
        }
        [HttpPost("CreateCategory")]
        public async Task<ActionResult<CategoryResponseDto>> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
        {
            try
            {
                var result = await _categoryService.CreateCategoryAsync(categoryCreateDto);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<ApiResponse<List<CategoryResponseDto>>>> GetAllCategories()
        {
            try
            {
                var result = await _categoryService.GetAllCategoryAsync();
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<CategoryResponseDto>> UpdateCategory([FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                var result = await _categoryService.UpdateCategoryAsync(categoryUpdateDto);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteCategory/{CategoryID:int}")]
        public async Task<ActionResult<CategoryResponseDto>> DeleteCategory(int CategoryID)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(CategoryID);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

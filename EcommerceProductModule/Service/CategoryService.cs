using AutoMapper;
using EcommerceProductModule.DataContext;
using EcommerceProductModule.Models;
using EcommerceProductModule.Models.Dtos;
using EcommerceProductModule.Models.Dtos.CategoryDto;
using EcommerceProductModule.Service.IService;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace EcommerceProductModule.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<CategoryResponseDto>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            try
            {
                var isAlreadyExist = await _context.Categories.FirstOrDefaultAsync(u => u.CategoryName.ToLower() == categoryCreateDto.CategoryName.ToLower());

                if (categoryCreateDto != null && isAlreadyExist==null)
                {
                    var category = _mapper.Map<Category>(categoryCreateDto);
                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync();
                    var isCategoryAdded = await _context.Categories.FirstOrDefaultAsync(u=>u.CategoryName.ToLower() == categoryCreateDto.CategoryName.ToLower());
                    if(isCategoryAdded!= null)
                    {
                        var categoryResponse = _mapper.Map<CategoryResponseDto>(isCategoryAdded);
                        return new ApiResponse<CategoryResponseDto>(200, true, categoryResponse, "Category details added successfully!");
                    }
                }
                return new ApiResponse<CategoryResponseDto>(400, false, "Category details are already exists.");
            }
            catch(Exception ex)
            {
                return new ApiResponse<CategoryResponseDto>(500,false,$"something went wrong : {ex}");
            }
        }

        public async Task<ApiResponse<CategoryResponseDto>> DeleteCategoryAsync(int CategoryID)
        {
            try
            {
                var isCategoryExist = await _context.Categories.FirstOrDefaultAsync(u => u.CategotyID == CategoryID);

                if (isCategoryExist != null)
                {
                    var isCategoryDeteted = _context.Categories.Remove(isCategoryExist);
                    await _context.SaveChangesAsync();

                    var isCategoryFound = await _context.Categories.FirstOrDefaultAsync(u => u.CategotyID == CategoryID);
                    if (isCategoryFound == null)
                    {
                        var categoryResponse = _mapper.Map<CategoryResponseDto>(isCategoryExist);
                        return new ApiResponse<CategoryResponseDto>(200, true, categoryResponse, "Category details deleted successfully!");
                    }
                }
                return new ApiResponse<CategoryResponseDto>(400, false, "Category details not found.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryResponseDto>(500, false, $"something went wrong : {ex}");
            }
        }

        public async Task<ApiResponse<List<CategoryResponseDto>>> GetAllCategoryAsync()
        {
            var allCategories = await _context.Categories.ToListAsync();

            var cat = _mapper.Map<List<CategoryResponseDto>>(allCategories);

            //var categoriesDto = allCategories.Select(category =>new CategoryResponseDto()
            //{
            //    CategotyID = category.CategotyID,
            //    CategoryName = category.CategoryName,
            //    Description = category.Description,
            //    IsActive = category.IsActive
            //}).ToList();

            return new ApiResponse<List<CategoryResponseDto>>(200,true,cat,"List of all the categories.");
        }

        public async Task<ApiResponse<CategoryResponseDto>> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                var isAlreadyExist = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(u => u.CategoryName.ToLower() == categoryUpdateDto.CategoryName.ToLower());

                if (categoryUpdateDto != null && isAlreadyExist != null)
                {
                    isAlreadyExist = _mapper.Map<Category>(categoryUpdateDto);
                    _context.Categories.Update(isAlreadyExist);
                    await _context.SaveChangesAsync();

                    var isCategoryUpdated = await _context.Categories.FirstOrDefaultAsync(u => u.CategoryName.ToLower() == categoryUpdateDto.CategoryName.ToLower());
                    if (isCategoryUpdated != null)
                    {
                        var categoryResponse = _mapper.Map<CategoryResponseDto>(isCategoryUpdated);
                        return new ApiResponse<CategoryResponseDto>(200, true, categoryResponse, "Category details updated successfully!");
                    }
                }
                return new ApiResponse<CategoryResponseDto>(400, false, "Category details not found.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryResponseDto>(500, false, $"something went wrong : {ex}");
            }
        }
    }
}

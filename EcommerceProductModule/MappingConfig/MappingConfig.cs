using AutoMapper;
using EcommerceProductModule.Models;
using EcommerceProductModule.Models.Dtos.CategoryDto;

namespace EcommerceProductModule.MappingConfig
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryResponseDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

        }
    }
}

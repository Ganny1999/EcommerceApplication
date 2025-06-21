using AutoMapper;
using EcommerceProductModule.Models;
using EcommerceProductModule.Models.Dtos.CategoryDto;
using EcommerceProductModule.Models.Dtos.ProductDto;

namespace EcommerceProductModule.MappingConfig
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryResponseDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductResponseDto>().ReverseMap();

        }
    }
}

using AutoMapper;
using EcommerceCartModule.Models;
using EcommerceCartModule.Models.Dtos;

namespace EcommerceCartModule.MappingConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
              CreateMap<Cart,AddCartDto>().ReverseMap();
              CreateMap<Cart,CartResponseDto>().ReverseMap();
              CreateMap<CartItem,CartItemResponseDto>().ReverseMap();
        }
    }
}

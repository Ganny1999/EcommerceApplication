using AutoMapper;
using EcommerceOrderModule.Models;
using EcommerceOrderModule.Models.Dtos;

namespace EcommerceOrderModule.MappingConfig
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
                CreateMap<Order,OrderResponseDto>().ReverseMap();
                CreateMap<OrderItem,OrderItemsResponseDto>().ReverseMap();
        }
    }
}

using EcommerceCartModule.Models;

using EcommerceCartModule.Models.Dtos;

namespace EcommerceCartModule.Service.IService
{
    public interface ICartService
    {
        Task<ApiResponse<CartResponseDto>> AddCartAsync(AddCartDto addCartDto);
    }
}

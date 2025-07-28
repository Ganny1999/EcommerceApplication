using EcommerceCartModule.Models;

using EcommerceCartModule.Models.Dtos;

namespace EcommerceCartModule.Service.IService
{
    public interface ICartService
    {
        Task<ApiResponse<CartResponseDto>> AddCartAsync(AddCartDto addCartDto);
        Task<ApiResponse<CartResponseDto>> UpdateCartAsync(UpdateCartDto updateCartDto);
        Task<ApiResponse<CartResponseDto>> GetCartAsync(int CartID);
        Task<ApiResponse<bool>> ClearCartAsync(string customerID);
    }
}

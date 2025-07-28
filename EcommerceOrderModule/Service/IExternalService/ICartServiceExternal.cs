using EcommerceOrderModule.Models;
using EcommerceOrderModule.Models.Dtos;

namespace EcommerceOrderModule.Service.IExternalService
{
    public interface ICartServiceExternal
    {
        Task<ApiResponse<CartResponseDto>> GetCart(int id);
        Task<ApiResponse<bool>> ClearCart(String CustomerID);
    }
}

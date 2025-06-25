using EcommerceCartModule.Models;
using EcommerceCartModule.Models.Dtos.ProductDto;

namespace EcommerceCartModule.Service.IService
{
    public interface IProductServiceExternal
    {
        Task<ApiResponse<ProductResponseDto>> GetProductByIDAsync(int ProductID);
    }
}

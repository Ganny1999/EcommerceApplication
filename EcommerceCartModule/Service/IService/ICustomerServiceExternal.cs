using EcommerceCartModule.Models;
using EcommerceCartModule.Models.Dtos.CustomerDto;

namespace EcommerceCartModule.Service.IService
{
    public interface ICustomerServiceExternal
    {
        Task<ApiResponse<CustomerResponseDTO>> GetCustomerByIDAsync(string CustomerID);
    }
}

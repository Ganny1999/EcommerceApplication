using EcommerceCustomerModule.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceCustomerModule.Service.IService
{
    public interface ICustomerService
    {
        Task<ApiResponse<CustomerResponseDTO>> RegisterCustomerAsync(CustomerRegistrationDTO customerRegistrationDTO);
        Task<ApiResponse<LoginResponseDTO>> LoginCustomerAsync(LoginDTO loginDTO);
        Task<ApiResponse<CustomerResponseDTO>> UpdateCustomerAsync(CustomerUpdateDTO customerUpdateDTO);
        Task<ApiResponse<CustomerResponseDTO>> DeleteCustomerAsync(string id);
        Task<ApiResponse<CustomerResponseDTO>> GetCustomerByIDAsync(string id);
        Task<ApiResponse<string>> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO);
    }
}

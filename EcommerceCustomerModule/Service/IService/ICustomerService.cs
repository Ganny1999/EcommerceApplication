using EcommerceCustomerModule.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceCustomerModule.Service.IService
{
    public interface ICustomerService
    {
        Task<ApiResponse<CustomerResponseDTO>> RegisterCustomerAsync(CustomerRegistrationDTO customerRegistrationDTO);
        Task<ApiResponse<LoginResponseDTO>> LoginCustomerAsync(LoginDTO loginDTO);
        Task<ApiResponse<CustomerResponseDTO>> UpdateCustomer(CustomerUpdateDTO customerUpdateDTO);
        Task<ApiResponse<CustomerResponseDTO>> UpdateCustomer(int id);
    }
}

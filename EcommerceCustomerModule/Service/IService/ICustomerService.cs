using EcommerceCustomerModule.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceCustomerModule.Service.IService
{
    public interface ICustomerService
    {
        Task<ApiResponse<CustomerResponseDTO>> RegisterCustomerAsync(CustomerRegistrationDTO customerRegistrationDTO);
    }
}

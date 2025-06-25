using EcommerceCartModule.Models;
using EcommerceCartModule.Models.Dtos.CustomerDto;
using EcommerceCartModule.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EcommerceCartModule.Service
{
    public class CustomerServiceExternal : ICustomerServiceExternal
    {
        private readonly HttpClient _httpClient;
        public CustomerServiceExternal(IHttpClientFactory httpClientFactory)
        {
                _httpClient = httpClientFactory.CreateClient("CustomerServiceClient");
        }
        public async Task<ApiResponse<CustomerResponseDTO>> GetCustomerByIDAsync(string CustomerID)
        {
            var CustomerResponse = await _httpClient.GetAsync($"/api/Customers/GetCustomerByID/{CustomerID}");
            var CustomerContent = await CustomerResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ApiResponse<CustomerResponseDTO>>(CustomerContent);

            return response;
        }
    }
}

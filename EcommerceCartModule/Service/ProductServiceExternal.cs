using EcommerceCartModule.Models;
using EcommerceCartModule.Models.Dtos.ProductDto;
using EcommerceCartModule.Service.IService;
using Newtonsoft.Json;

namespace EcommerceCartModule.Service
{
    public class ProductServiceExternal : IProductServiceExternal
    {
        private readonly HttpClient _httpClient;
        public ProductServiceExternal(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductServiceClient");
        }
        public async Task<ApiResponse<ProductResponseDto>> GetProductByIDAsync(int ProductID)
        {
            var productResponse = await _httpClient.GetAsync($"/api/Product/GetProductByID/{ProductID}");
            var content = await productResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ApiResponse<ProductResponseDto>>(content);

            return response;
        }
    }
}

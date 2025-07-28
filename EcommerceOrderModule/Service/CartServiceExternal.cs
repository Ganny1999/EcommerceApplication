using EcommerceOrderModule.Models;
using EcommerceOrderModule.Models.Dtos;
using EcommerceOrderModule.Service.IExternalService;
using Newtonsoft.Json;

namespace EcommerceOrderModule.Service
{
    public class CartServiceExternal : ICartServiceExternal
    {
        private readonly HttpClient _httpClient;
        public CartServiceExternal(IHttpClientFactory httpClientFactory)
        {
                _httpClient = httpClientFactory.CreateClient("CartServiceClient");
        }
        public async Task<ApiResponse<CartResponseDto>> GetCart(int CartID)
        {
            var cartResponse = await _httpClient.GetAsync($"/api/Cart/GetCart/{CartID}");
            var content = await cartResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ApiResponse<CartResponseDto?>>(content);

            return response;
        }
        public async Task<ApiResponse<bool>> ClearCart(String CustomerID)
        {
            var cartResponse = await _httpClient.GetAsync($"/api/Cart/ClearCart/{CustomerID}");
            var content = await cartResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ApiResponse<bool>>(content);

            return response;
        }
    }
}

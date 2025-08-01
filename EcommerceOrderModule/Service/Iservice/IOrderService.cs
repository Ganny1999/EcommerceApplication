using EcommerceOrderModule.Models;
using EcommerceOrderModule.Models.Dtos;

namespace EcommerceOrderModule.Service.Iservice
{
    public interface IOrderService
    {
        public byte[] GenerateDailyOrderReportExcel(DateTime dateTime);

        public Task<ApiResponse<OrderResponseDto>> PlaceOrderAsync(int CartID);
        public Task<ApiResponse<OrderResponseDto>> GetOrderByIDAsync(string OrderID);
        public bool OrderStatusManagementAsync(string OrderID);
        public Task<ApiResponse<OrderResponseDto>> GetOrderByCustomerAsync(string CUstomerID);
    }
}

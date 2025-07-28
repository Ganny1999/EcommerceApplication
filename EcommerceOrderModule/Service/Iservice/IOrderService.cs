using EcommerceOrderModule.Models;
using EcommerceOrderModule.Models.Dtos;

namespace EcommerceOrderModule.Service.Iservice
{
    public interface IOrderService
    {
        public byte[] GenerateDailyOrderReportExcel(DateTime dateTime);

        public Task<ApiResponse<OrderResponseDto>> PlaceOrderAsync(int CartID);
        public OrderResponseDto GetOrderByIDAsync(string OrderID);
        public bool OrderStatusManagementAsync(string OrderID);
        public OrderResponseDto GetOrderByCustomerAsync(string CUstomerID);
    }
}

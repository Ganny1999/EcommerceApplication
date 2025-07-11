using EcommerceOrderModule.Service.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceOrderModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
                _orderService = orderService;
        }
        [HttpPost("daily-excel")] 
        public IActionResult GetDailyOrderExcel([FromBody] DateTime? dateTime)
        {
            return NoContent();

            //var reportDate = dateTime ?? DateTime.Today;
            //var fileByte = _orderService.GenerateDailyOrderReportExcel(reportDate);
            //var fileName = $"DailyReport_{reportDate:yyyy-MM-dd}.xlsx";

            //return File(fileByte, "application/vnd.api+json", fileName);
        }
    }
}

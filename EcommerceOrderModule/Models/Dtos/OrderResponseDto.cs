using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceOrderModule.Models.Dtos
{
    public class OrderResponseDto
    {
        public string OrderID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
        public enum OrderStatus
        {
            Pending = 1,
            Processing = 2,
            Success = 3,
            Cancel = 4
        }
        [NotMapped]
        public ICollection<OrderItemsResponseDto> OrderItems { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EcommerceOrderModule.Models.Dtos
{
    public class OrderItemsResponseDto
    {
        public string OderItemID { get; set; }
        public string OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

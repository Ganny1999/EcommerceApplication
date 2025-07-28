using System.ComponentModel.DataAnnotations;

namespace EcommerceOrderModule.Models
{
    public class OrderItem
    {
        [Key]
        public string OderItemID { get; set; }
        [Required(ErrorMessage ="Order ID required.")]
        public string OrderID { get; set; }
        [Required(ErrorMessage = "Product ID required.")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity {  get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "TotalPrice is required.")]
        public decimal TotalPrice {  get; set; }
    }
}

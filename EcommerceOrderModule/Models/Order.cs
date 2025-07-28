using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceOrderModule.Models
{
    public class Order
    {
        [Required]
        public string OrderID {  get; set; }
        [Required(ErrorMessage ="Order number is required.")]
        public string OrderNumber { get; set; }
        [Required(ErrorMessage = "Order date is required.")]
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "Customer is required.")]
        public string CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
        public  enum OrderStatus
        {
            Pending = 1,
            Processing = 2,
            Success = 3,
            Cancel = 4
        }
        [NotMapped]
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}

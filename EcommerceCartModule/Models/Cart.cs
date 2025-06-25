using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCartModule.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string CustomerId { get; set; }
        public bool IsCheckedOut { get; set; } = false;
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [NotMapped]
        public ICollection<CartItem> CartItems { get; set; }
    }
}

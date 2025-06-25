using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCartModule.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        [Required]
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
        public int Quantity { get; set; }
        [Required]
        [NotMapped]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 10000.00, ErrorMessage = "Unit Price must be between $0.01 and $10,000.00.")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalItemPrice { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}

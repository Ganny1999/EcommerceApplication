namespace EcommerceCartModule.Models.Dtos
{
    public class CartResponseDto
    {
        public int CartId { get; set; }
        public string? CustomerId { get; set; }
        public bool IsCheckedOut { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartItemResponseDto>? CartItems { get; set; }
    }
}

namespace EcommerceOrderModule.Models
{
    public class Order
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

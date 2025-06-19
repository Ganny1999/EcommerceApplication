using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceCustomerModule.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }  
        public string CustomerID {  get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }
        [Required(ErrorMessage = "Address Line 1 is required.")]
        [StringLength(50, ErrorMessage = "Address Line 1 cannot exceed 50 characters.")]
        public string AddressLine1 {  get; set; }
        [Required(ErrorMessage = "Address Line 2 is required.")]
        [StringLength(50, ErrorMessage = "Address Line 2 cannot exceed 50 characters.")]
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        public string City { get; set;}
        [Required(ErrorMessage = "State is required.")]
        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters.")]
        public string State { get; set;}
        [Required(ErrorMessage = "PostalCode is required.")]
        [StringLength(50, ErrorMessage = "PostalCode cannot exceed 50 characters.")]
        public string PostalCode { get; set;}
        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        public string Country { get; set;}
    }
}

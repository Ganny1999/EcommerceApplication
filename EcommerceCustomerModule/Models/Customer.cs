using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceCustomerModule.Models
{
    public class Customer : IdentityUser
    {
        [NotMapped]
        public string CustomerID { get; set; }
        [Required(ErrorMessage ="First Name is required.")]
        [StringLength(50, MinimumLength =2,ErrorMessage ="First Name must be between 2 to 50.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Last Name is required.")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Last Name must be between 2 to 50.")]
        public string LastName { get; set; }
        //[Required(ErrorMessage = "Email is required.")]
        //public string Email { get; set; }
        //[Required(ErrorMessage = "Phone Number is required.")]
        //public string PhoneNumber { get; set; }
        //[Required(ErrorMessage = "Passward is required.")]
        //public string Passward { get; set; }
        public bool isActive { get; set; }
        [NotMapped] 
        public ICollection<Address> Addresses { get; set; }
        [NotMapped]
        public ICollection<Order> Orders { get; set; }

        // Navigation property: A user can have many carts but only 1 active cart
        [NotMapped]
        public ICollection<Cart> Carts { get; set; }
        [NotMapped]
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}

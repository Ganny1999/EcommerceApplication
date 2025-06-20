using EcommerceProductModule.Service.IService;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceProductModule.Models
{
    public class Category
    {
        [Key]
        public int CategotyID { get; set; }
        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(100,MinimumLength =2,ErrorMessage = "Category Name must be between 3 and 100 characters.")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage ="Description is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Description Name must be between 3 and 100 characters.")]
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        [NotMapped]
        public ICollection<Product> products { get; set; }  
    }
}

using System.ComponentModel.DataAnnotations;

namespace EcommerceProductModule.Models.Dtos.CategoryDto
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category Name must be between 3 and 100 characters.")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Description Name must be between 3 and 100 characters.")]
        public string Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EcommerceProductModule.Models.Dtos.CategoryDto
{
    public class CategoryResponseDto
    {
        public int CategotyID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

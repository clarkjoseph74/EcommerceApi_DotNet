using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class CategoryCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class ProductCreateDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}

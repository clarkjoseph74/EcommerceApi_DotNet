using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}

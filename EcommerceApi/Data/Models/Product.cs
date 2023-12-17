using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}

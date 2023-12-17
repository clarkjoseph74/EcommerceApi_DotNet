using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Data.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]

        public int Quantity { get; set; }
        [Required]

        public decimal Price { get; set; }
        public decimal totalPrice { get; set; }
        public Product Product { get; set; }
    }
}

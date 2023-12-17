using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class CartCreateDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]

        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
       
    }
}

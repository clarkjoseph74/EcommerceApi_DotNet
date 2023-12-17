using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(100)]
        public string username { get; set; }
        [Required]

        public string password { get; set; }
        [Required]

        public string email { get; set; }
    }
}

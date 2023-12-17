using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class LoginDTO
    {
        [Required]
        public string username { get; set; }
        [Required]

        public string password { get; set; }
    }
}

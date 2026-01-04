using System.ComponentModel.DataAnnotations;

namespace BabyShopWeb2.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username harus diisi")]
        public string Username { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Password harus diisi")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        
        public bool RememberMe { get; set; }
    }
}

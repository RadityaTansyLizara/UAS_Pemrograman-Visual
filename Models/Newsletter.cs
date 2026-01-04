using System;
using System.ComponentModel.DataAnnotations;

namespace BabyShopWeb2.Models
{
    public class Newsletter
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; } = string.Empty;
        
        public DateTime SubscribedAt { get; set; } = DateTime.Now;
        
        public bool IsActive { get; set; } = true;
    }
}

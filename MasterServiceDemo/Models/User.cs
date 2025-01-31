using System.ComponentModel.DataAnnotations;

namespace MasterServiceDemo.Models
{
    public class User
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(6)]
        public string LoginOTP { get; set; }
    }
}

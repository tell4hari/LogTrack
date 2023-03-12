using System.ComponentModel.DataAnnotations;

namespace LogTracker.Models
{
    public class UserViewModel
    {
        [Key]
        public  int Id { get; set; }
        [Required]
        public string LoginName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        [Required]
        public int IsAdmin { get; set; } = 0;
              
    }
}

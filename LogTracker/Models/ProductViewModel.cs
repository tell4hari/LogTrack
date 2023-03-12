using System.ComponentModel.DataAnnotations;

namespace LogTracker.Models
{
    public class ProductViewModel
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? GameName { get; set; }
        public string? Desc { get; set; }
        public string? SKU { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }= DateTime.Now;
        [Required]
        public DateTime? EndDate { get; set; }=DateTime.Now;
        [Required]
        public double Price { get; set; } = 0;
        [Required]
        public int SoldCount { get; set;}
        [Required]
        public int SoldValue { get; set; }
        
        [Required]
        public DateTime CredatedDate { get; set; }
        public int CredatedBy { get; set; }

    }
}

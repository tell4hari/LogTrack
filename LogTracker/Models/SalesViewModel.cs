using System.ComponentModel.DataAnnotations;

namespace LogTracker.Models
{
    public class SalesViewModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }

        public string? LoginName { get; set; }
        [Required]
        public DateTime? StartDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public int SoldCount { get; set; } = 0;
        [Required]
        public double SoldValue { get; set; } = 0;
        [Required]
        public int ShiftId { get; set; } = 1;

        [Required]
        public string? CreatedBy { get; set; }
        [Required]

        public DateTime CreatedDate { get; set; }

    }
}

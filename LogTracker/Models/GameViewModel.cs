
using System.ComponentModel.DataAnnotations;

namespace LogTracker.Models
{
    public class GameViewModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }    
        public string? Desc { get; set; } 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? SoldCount { get; set; }
        public double? SoldValue { get; set; }
    }
}

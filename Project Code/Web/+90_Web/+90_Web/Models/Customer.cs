using System.ComponentModel.DataAnnotations;

namespace _90_Web.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        public int Points { get; set; } = 0;

        public double TotalHours { get; set; } = 0;

        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
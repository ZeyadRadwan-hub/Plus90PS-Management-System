using System.ComponentModel.DataAnnotations;

namespace _90_Web.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public bool IsPaid { get; set; } = true;

        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
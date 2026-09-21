using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace _90_Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Precision(18, 2)]
        public decimal BuyPrice { get; set; }

        [Precision(18, 2)]
        public decimal SellPrice { get; set; }

        public int Stock { get; set; }

        public int MinStock { get; set; }

        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
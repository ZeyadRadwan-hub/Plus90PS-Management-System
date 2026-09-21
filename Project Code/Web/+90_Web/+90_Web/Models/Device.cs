using System.ComponentModel.DataAnnotations;

namespace _90_Web.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
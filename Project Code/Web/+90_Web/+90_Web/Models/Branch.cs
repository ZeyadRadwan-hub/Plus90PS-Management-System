using System.ComponentModel.DataAnnotations;

namespace _90_Web.Models
{
    public class Branch
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
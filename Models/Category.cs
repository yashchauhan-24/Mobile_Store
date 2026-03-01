using System.ComponentModel.DataAnnotations;

namespace Mobile_Store.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}

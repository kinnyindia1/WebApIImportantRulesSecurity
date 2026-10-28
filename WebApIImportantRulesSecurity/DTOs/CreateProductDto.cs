using System.ComponentModel.DataAnnotations;

namespace WebApIImportantRulesSecurity.DTOs
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [Range(0.01, 999999, ErrorMessage = "Price must be between 0.01 and 999999")]
        public decimal Price { get; set; }
    }
}

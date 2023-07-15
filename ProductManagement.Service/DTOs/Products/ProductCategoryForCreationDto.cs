using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Service.DTOs.Products;
public class ProductCategoryForCreationDto
{
    [Required]
    public long ProductId { get; set; }
    [Required]
    public long CategoryId { get; set; }
}

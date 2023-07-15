using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Service.DTOs.Products;
public class CategoryForCreationDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(30, MinimumLength = 2)]
    public string Name { get; set; }
}

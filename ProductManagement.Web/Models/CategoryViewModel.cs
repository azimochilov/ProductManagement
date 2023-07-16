using ProductManagement.Service.DTOs.Products;

namespace ProductManagement.Web.Models;
public class CategoryViewModel
{
    public CategoryForResultDto Result { get; set; }
    public CategoryForCreationDto Creation { get; set; }
}

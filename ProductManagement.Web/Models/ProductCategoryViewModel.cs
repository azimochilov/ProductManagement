using ProductManagement.Service.DTOs.Products;

namespace ProductManagement.Web.Models;
public class ProductCategoryViewModel
{
    public ProductCategoryForResultDto Result { get; set; }
    public ProductCategoryForCreationDto Creation { get; set; }
}

using ProductManagement.Service.DTOs.Products;

namespace ProductManagement.Web.Models;
public class ProductViewModel
{
    public ProductForResultDto Result { get; set; }
    public ProductForCreationDto Creation { get; set; }
}

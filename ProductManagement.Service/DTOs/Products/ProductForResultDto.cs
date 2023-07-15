using ProductManagement.Domain.Entities;

namespace ProductManagement.Service.DTOs.Products;
public class ProductForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Serial { get; set; }
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
    public long CategoryId { get; set; }
    public ProductCategory Category { get; set; }
}

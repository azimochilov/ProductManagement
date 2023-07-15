using ProductManagement.Domain.Entities;

namespace ProductManagement.Service.DTOs.Products;
public class ProductCategoryForResultDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public ProductForResultDto Product { get; set; }

    public long CategoryId { get; set; }
    public CategoryForResultDto Category { get; set; }
}

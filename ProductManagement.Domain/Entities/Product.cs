using ProductManagement.Domain.Commons;

namespace ProductManagement.Domain.Entities;
public class Product : Auditable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Weight { get; set; }

    public long CategoryId { get; set; }
    public ProductCategory Category { get; set; }
}

using ProductManagement.Domain.Commons;

namespace ProductManagement.Domain.Entities;
public class ProductCategory : Auditable
{
    public long ProductId { get; set; }
    public Product Product { get; set; }

    public long CategoryId { get; set; }
    public Category Category { get; set; }
}
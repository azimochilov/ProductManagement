using ProductManagement.Domain.Commons;

namespace ProductManagement.Domain.Entities;
public class Category : Auditable
{
    public string Name { get; set; }
}

using ProductManagement.Service.DTOs.Products;

namespace ProductManagement.Service.Interfaces;
public interface IProductService
{
    public Task<ProductForResultDto> AddAsync(ProductForCreationDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<ProductForResultDto> ModifyAsync(long id, ProductForCreationDto dto);
    public Task<IEnumerable<ProductForResultDto>> RetrieveAllAsync();
    public Task<ProductForResultDto> RetrieveByIdAsync(long id);
}

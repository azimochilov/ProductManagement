using ProductManagement.Service.DTOs.Products;

namespace ProductManagement.Service.Interfaces;
public interface IProductCategoryService
{
    public Task<ProductCategoryForResultDto> AddAsync(ProductCategoryForCreationDto dto);
    public Task<ProductCategoryForResultDto> ModifyAsync(long id, ProductCategoryForCreationDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<ProductCategoryForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<ProductCategoryForResultDto>> RetrieveAllAsync();
}

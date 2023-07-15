using ProductManagement.Service.DTOs.Products;

namespace ProductManagement.Service.Interfaces;
public interface ICategoryService
{
    public Task<CategoryForResultDto> AddAsync(CategoryForCreationDto dto);
    public Task<CategoryForResultDto> ModifyAsync(long id, CategoryForCreationDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<CategoryForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<CategoryForResultDto>> RetrieveAllAsync();
}

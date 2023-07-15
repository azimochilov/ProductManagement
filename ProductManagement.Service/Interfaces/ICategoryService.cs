using ProductManagement.Service.DTOs.Products;

namespace ProductManagement.Service.Interfaces;
public interface ICategoryService
{
    public ValueTask<CategoryForResultDto> AddAsync(CategoryForCreationDto dto);
    public ValueTask<CategoryForResultDto> ModifyAsync(long id, CategoryForCreationDto dto);
    public ValueTask<bool> RemoveAsync(long id);
    public ValueTask<CategoryForResultDto> RetrieveByIdAsync(long id);
    public ValueTask<IEnumerable<CategoryForResultDto>> RetrieveAllAsync();
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data.IRepositories;
using ProductManagement.Domain.Entities;
using ProductManagement.Service.DTOs.Products;
using ProductManagement.Service.Exceptions;
using ProductManagement.Service.Interfaces;

namespace ProductManagement.Service.Services;
public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> categoryRepository;
    private readonly IMapper mapper;

    public CategoryService(IMapper mapper, IRepository<Category> categoryRepository)
    {
        this.mapper = mapper;
        this.categoryRepository = categoryRepository;
    }

    public async ValueTask<CategoryForResultDto> AddAsync(CategoryForCreationDto dto)
    {
        var category = await categoryRepository.SelectAsync(srn => srn.Name == dto.Name);
        if (category is not null && !category.IsDeleted)
            throw new AppException(409, "Category Already exists");

        var mappedCategory = mapper.Map<Category>(dto);
        mappedCategory.CreatedAt = DateTime.UtcNow;
        var addedCategory = await categoryRepository.InsertAsync(mappedCategory);

        await categoryRepository.SaveAsync();

        return mapper.Map<CategoryForResultDto>(addedCategory);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {

        var category = await categoryRepository.SelectAsync(p => p.Id == id);
        if (category is null || category.IsDeleted)
            throw new AppException(404, "Couldn't find category for this given Id");

        await categoryRepository.DeleteAsync(p => p.Id == id);
        await categoryRepository.SaveAsync();

        return true;
    }
    public async ValueTask<IEnumerable<CategoryForResultDto>> RetrieveAllAsync()
    {
        var categories = await categoryRepository.SelectAll()
            .Where(p => !p.IsDeleted)
            .ToListAsync();

        return mapper.Map<IEnumerable<CategoryForResultDto>>(categories);

    }
    public async ValueTask<CategoryForResultDto> RetrieveByIdAsync(long id)
    {
        var category = await categoryRepository.SelectAsync(p => p.Id == id);

        if (category is null || category.IsDeleted)
            throw new AppException(404, "Category Not Found");

        return mapper.Map<CategoryForResultDto>(category);
    }
    public async ValueTask<CategoryForResultDto> ModifyAsync(long id, CategoryForCreationDto dto)
    {
        var category = await categoryRepository.SelectAsync(p => p.Id == id);
        if (category is null || category.IsDeleted)
            throw new AppException(404, "Couldn't found category for given Id");

        var modifiedCategory = mapper.Map(dto, category);
        modifiedCategory.UpdatedAt = DateTime.UtcNow;

        await categoryRepository.SaveAsync();

        return mapper.Map<CategoryForResultDto>(modifiedCategory);
    }
}

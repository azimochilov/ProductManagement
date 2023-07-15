using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data.IRepositories;
using ProductManagement.Domain.Entities;
using ProductManagement.Service.DTOs.Products;
using ProductManagement.Service.Exceptions;
using ProductManagement.Service.Interfaces;

namespace ProductManagement.Service.Services;
public class ProductCategoryService : IProductCategoryService
{
    private readonly IRepository<ProductCategory> productCategoryRepository;
    private readonly IMapper mapper;

    public ProductCategoryService(IMapper mapper, IRepository<ProductCategory> productCategoryRepository)
    {
        this.mapper = mapper;
        this.productCategoryRepository = productCategoryRepository;
    }
    public async Task<ProductCategoryForResultDto> AddAsync(ProductCategoryForCreationDto dto)
    {
        var productCategory = await productCategoryRepository.SelectAsync(u => u.CategoryId .Equals(dto.CategoryId) && u.ProductId.Equals(dto.ProductId));
        if (productCategory is not null && !productCategory.IsDeleted)
            throw new AppException(409, "ProductCategory Already exists");

        var mappedCategory = mapper.Map<ProductCategory>(dto);
        mappedCategory.CreatedAt = DateTime.UtcNow;
        var addedProduct = await productCategoryRepository.InsertAsync(mappedCategory);

        await productCategoryRepository.SaveAsync();

        return mapper.Map<ProductCategoryForResultDto>(addedProduct);
    }
    public async Task<bool> RemoveAsync(long id)
    {

        var productCategory = await productCategoryRepository.SelectAsync(p => p.Id == id);
        if (productCategory is null || productCategory.IsDeleted)
            throw new AppException(404, "Couldn't find product category for this given Id");

        await productCategoryRepository.DeleteAsync(p => p.Id == id);
        await productCategoryRepository.SaveAsync();

        return true;
    }
    public async Task<IEnumerable<ProductCategoryForResultDto>> RetrieveAllAsync()
    {
        var productCategories = await productCategoryRepository.SelectAll()
            .Where(p => !p.IsDeleted)
            .ToListAsync();

        return mapper.Map<IEnumerable<ProductCategoryForResultDto>>(productCategories);

    }
    public async Task<ProductCategoryForResultDto> RetrieveByIdAsync(long id)
    {
        var productCategory = await productCategoryRepository.SelectAsync(p => p.Id == id);

        if (productCategory is null || productCategory.IsDeleted)
            throw new AppException(404, "Product Not Found");

        return mapper.Map<ProductCategoryForResultDto>(productCategory);
    }
    public async Task<ProductCategoryForResultDto> ModifyAsync(long id, ProductCategoryForCreationDto dto)
    {
        var productCategory = await productCategoryRepository.SelectAsync(p => p.Id == id);
        if (productCategory is null || productCategory.IsDeleted)
            throw new AppException(404, "Couldn't found product category for given Id");

        var modifiedProductCategory = mapper.Map(dto, productCategory);
        modifiedProductCategory.UpdatedAt = DateTime.UtcNow;

        await productCategoryRepository.SaveAsync();

        return mapper.Map<ProductCategoryForResultDto>(modifiedProductCategory);
    }
}

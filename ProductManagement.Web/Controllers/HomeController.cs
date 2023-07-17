using Microsoft.AspNetCore.Mvc;
using ProductManagement.Service.DTOs.Products;
using ProductManagement.Service.Exceptions;
using ProductManagement.Service.Interfaces;
using ProductManagement.Service.Services;
using ProductManagement.Web.Models;
using System.Diagnostics;

namespace ProductManagement.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService productService;
    private readonly ICategoryService categoryService;
    private readonly IProductCategoryService categoryProductService;
    public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, IProductCategoryService categoryProductService)
    {
        _logger = logger;
        this.productService = productService;
        this.categoryService = categoryService;
        this.categoryProductService = categoryProductService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await this.productService.RetrieveAllAsync();
        return View(products.ToList());
    }
    public IActionResult ProductCraete()
    {
        return View(); 
    }
    // POST: Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductCreate(ProductForCreationDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var addedProduct = await productService.AddAsync(dto);
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        return View(dto);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult ProductCreate()
    {
        return View();
    }
    // GET: Home/ProductEdit
    [HttpGet]
    [Route("ProductEdit")]
    public IActionResult ProductEdit()
    {
        return View();
    }

    // GET: Home/ProductEdit/{id}
    [HttpGet]
    [Route("ProductEdit/{id:long}")]
    public async Task<IActionResult> ProductEdit(long id)
    {
        try
        {
            var product = await productService.RetrieveByIdAsync(id);
            var dto = new ProductForCreationDto
            {
                Name = product.Name,
                Price = product.Price,
            };
            return View(dto);
        }
        catch (AppException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: Home/ProductEdit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("ProductEdit/{id:long}")]
    public async Task<IActionResult> ProductEdit(long id, ProductForCreationDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var modifiedProduct = await productService.ModifyAsync(id, dto);
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {
                return NotFound(ex.Message);
            }
        }

        return View(dto);
    }
    // GET: Home/ProductDelete/{id}
    [HttpGet("ProductDelete/{id}")]
    public async Task<IActionResult> ProductDelete(long id)
    {
        try
        {
            var product = await productService.RetrieveByIdAsync(id);
            var dto = new ProductForResultDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
            };
            return View(dto);
        }
        catch (AppException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: Product/ProductDeleteConfirmed/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("ProductDeleteConfirmed/{id}")]
    public async Task<IActionResult> ProductDeleteConfirmed(long id)
    {
        try
        {
            await productService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
        catch (AppException ex)
        {
            return NotFound(ex.Message);
        }
    }

    
    
    // CATEGORY
    
    
    public async Task<IActionResult> Category()
    {
        var categories = await categoryService.RetrieveAllAsync();
        return View(categories.ToList());
    }
    public IActionResult CreateCategory()
    {
        return View();
    }    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory(CategoryForCreationDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var addedCategory = await categoryService.AddAsync(dto);
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        return View(dto);
    }
    // GET: Home/EditCategory
    [HttpGet]
    [Route("EditCategory")]
    public IActionResult EditCategory()
    {
        return View();
    }

    // GET: Home/EditCategory/{id}
    [HttpGet]
    [Route("EditCategory/{id:long}")]
    public async Task<IActionResult> EditCategory(long id)
    {
        try
        {
            var category = await categoryService.RetrieveByIdAsync(id);
            var dto = new CategoryForCreationDto
            {
                Name = category.Name,                                
            };
            return View(dto);
        }
        catch (AppException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: Home/EditCategory/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("EditCategory/{id:long}")]
    public async Task<IActionResult> EditCategory(long id, CategoryForCreationDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var modifiedCategory = await categoryService.ModifyAsync(id, dto);
                return RedirectToAction("Category");
            }
            catch (AppException ex)
            {
                return NotFound(ex.Message);
            }
        }

        return View(dto);
    }

    // GET: Home/ProductCategoryDelete/{id}
    [HttpGet("ProductCategoryyDelete/{id}")]
    public async Task<IActionResult> DeleteCategory(long id)
    {
        try
        {
            var category = await categoryService.RetrieveByIdAsync(id);
            var dto = new CategoryForResultDto
            {
                Id = category.Id,
                Name = category.Name,                               
            };
            return View(dto);
        }
        catch (AppException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: Product/ProductCategoryDeleteConfirmed/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("ProductCategoryyDeleteConfirmed/{id}")]
    public async Task<IActionResult> DeleteCategoryConfirmed(long id)
    {
        try
        {
            await categoryService.RemoveAsync(id);
            return RedirectToAction("Category");
        }
        catch (AppException ex)
        {
            return NotFound(ex.Message);
        }
    }


    // PRODUCT CATEGORY


    public async Task<IActionResult> ProductCategory()
    {
        var categories = await categoryProductService.RetrieveAllAsync();
        return View(categories.ToList());
    }

    public IActionResult ProductCategoryCreate()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductCategoryCreate(ProductCategoryForCreationDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var addedProductCategory = await categoryProductService.AddAsync(dto);
                var productCategories = await categoryProductService.RetrieveAllAsync();
                var productCategoriesDto = productCategories.Select(pc => new ProductCategoryForResultDto
                {
                    Id = pc.Id,
                    ProductId = pc.ProductId,
                    Product = new ProductForResultDto
                    {
                        Id = pc.Product.Id,
                        Name = pc.Product.Name,
                        Price = pc.Product.Price,
                        Weight = pc.Product.Weight,
                        Serial = pc.Product.Serial
                    },
                    Category = new CategoryForResultDto
                    {
                        Id = pc.Category.Id,
                        Name = pc.Category.Name
                    }
                });

                return RedirectToAction("ProductCategory");
            }
            catch (AppException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        return View(dto);
    }

    // GET: Home/ProductCategoryEdit
    [HttpGet]
    [Route("ProductCategoryEdit")]
    public IActionResult ProductCategoryEdit()
    {
        return View();
    }

    // GET: Home/ProductCategoryEdit/{id}
    [HttpGet]
    [Route("ProductCategoryEdit/{id:long}")]
    public async Task<IActionResult> ProductCategoryEdit(long id)
    {
        try
        {
            var category = await categoryProductService.RetrieveByIdAsync(id);
            var dto = new ProductCategoryForCreationDto
            {
                ProductId = category.ProductId,
                CategoryId= category.CategoryId,
            };
            return View(dto);
        }
        catch (AppException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: Home/ProductCategoryEdit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("ProductCategoryEdit/{id:long}")]
    public async Task<IActionResult> ProductCategoryEdit(long id, ProductCategoryForCreationDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var modifiedCategory = await categoryProductService.ModifyAsync(id, dto);
                return RedirectToAction("ProductCategory");
            }
            catch (AppException ex)
            {
                return NotFound(ex.Message);
            }
        }

        return View(dto);
    }

    // GET: Home/ProductCategoryDelete/{id}
    [HttpGet("ProductCategoryDelete/{id}")]
    public async Task<IActionResult> ProductCategoryDelete(long id)
    {
        try
        {
            var category = await categoryProductService.RetrieveByIdAsync(id);
            var dto = new ProductCategoryForResultDto
            {
                Id = id,
                CategoryId= category.CategoryId,
                ProductId = category.ProductId,                
                
            };
            return View(dto);
        }
        catch (AppException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: Product/ProductCategoryDeleteConfirmed/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("ProductCategoryDeleteConfirmed/{id}")]
    public async Task<IActionResult> ProductCategoryDeleteConfirmed(long id)
    {
        try
        {
            await categoryProductService.RemoveAsync(id);
            return RedirectToAction("ProductCategory");
        }
        catch (AppException ex)
        {
            return NotFound(ex.Message);
        }
    }


}
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Service.DTOs.Products;
using ProductManagement.Service.Exceptions;
using ProductManagement.Service.Interfaces;
using ProductManagement.Service.Services;
using ProductManagement.Web.Models;
using System.Diagnostics;

namespace ProductManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            this.productService = productService;
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

        public IActionResult Privacy()
        {
            return View();
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
                    // Map other properties as needed
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
                    // Map other properties as needed
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


       // GET: Home/AnotherProductDelete
       [HttpGet]
        [Route("AnotherProductDelete")]
        public IActionResult AnotherProductDelete()
        {
            return View();
        }



    }
}
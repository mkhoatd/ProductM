using Microsoft.AspNetCore.Mvc;
using ProductM.Models;
using ProductM.Services;

namespace ProductM.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetProducts();
            return View(products);
        }
        
        public IActionResult Create()
        {
            var categories = _productService.GetCategories();
            return View(categories);
        }

        public IActionResult Save(Product product)
        {
            if(product.Id == 0) _productService.CreateProduct(product);
            else _productService.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var product = _productService.GetProductById(id);
            if(product == null) return RedirectToAction("Create");
            ViewBag.Product = product; //test cach 2 viewbag
            var categories = _productService.GetCategories();
            return View(categories);
        }

        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
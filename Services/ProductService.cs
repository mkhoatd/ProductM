using ProductM.Models;

namespace ProductM.Services
{
    public class ProductService : IProductService
    {
         private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var existedProduct = GetProductById(id);
            if(existedProduct == null) return;
            _context.Products.Remove(existedProduct);
            _context.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return _context.Category.ToList();
        }

        public Product? GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public void UpdateProduct(Product product)
        {
            var existedProduct = GetProductById(product.Id);
            if(existedProduct == null) return;
            existedProduct.Name = product.Name;
            existedProduct.Slug = product.Slug;
            existedProduct.Quantity = product.Quantity;
            existedProduct.Price = product.Price;
            existedProduct.CategoryId = product.CategoryId;
            _context.Products.Update(existedProduct);
            _context.SaveChanges();
        }
    }
}
using ProductM.Models;

namespace ProductM.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product? GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        List<Category> GetCategories();
    }
}
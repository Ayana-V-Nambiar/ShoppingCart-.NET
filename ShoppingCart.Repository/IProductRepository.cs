using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.ShoppingCart.Services
{
    public interface IProductRepository
    {
        Product GetProduct(int productId);//synchronous
        List<Product> GetProducts(int noOfProdducts);

        Task<Product> GetProductAsync(int productId);//asynchronous
        Task<List<Product>> GetProductsAsync(int noOfProdducts);
        
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int productId);
        Task<Product> GetProductByNameAsync(string name);
    }
}
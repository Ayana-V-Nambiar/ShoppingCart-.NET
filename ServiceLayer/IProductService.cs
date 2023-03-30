using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;
using ShoppingCart.ViewModel.Get;

namespace ShoppingCart.ServiceLayer
{
    public interface IProductService
    {
        Product GetProduct(int productId);//synchronous
        List<Product> GetProducts(int noOfProdducts = 100);

        Task<Product> GetProductAsync(int productId);//asynchronous
        Task<List<Product>> GetProductsAsync(int noOfProdducts = 100);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int productId);
        Task<bool> IsProductNameExistAsync(string name);
       // Task UpdateProductAsync(ProductViewModel entityToUpdate);
    }
}
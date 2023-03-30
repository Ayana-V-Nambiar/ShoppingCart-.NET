using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;
using ShoppingCart.ShoppingCart.Services;
using ShoppingCart.ViewModel.Get;

namespace ShoppingCart.ServiceLayer
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            
        }
        public Task<Product> CreateProductAsync(Product product)
        {
            return productRepository.CreateProductAsync(product);
        }

        public Task<bool> DeleteProductAsync(int productId)
        {
            return productRepository.DeleteProductAsync(productId);
        }

        public Task<Product> UpdateProductAsync(Product product)
        {
            return productRepository.UpdateProductAsync(product);
        }
        public Product GetProduct(int productId)
        {
            return productRepository.GetProduct(productId);
        }

        public Task<Product> GetProductAsync(int productId)
        {
            return productRepository.GetProductAsync(productId);
        }

        public List<Product> GetProducts(int noOfProdducts = 100)
        {
            return productRepository.GetProducts(noOfProdducts);
        }

        public Task<List<Product>> GetProductsAsync(int noOfProdducts = 100)
        {
            return productRepository.GetProductsAsync(noOfProdducts);
        }

        public async  Task<bool> IsProductNameExistAsync(string name)
        {
           var product = await productRepository.GetProductByNameAsync(name);
           return product != null;
        }
    }
}
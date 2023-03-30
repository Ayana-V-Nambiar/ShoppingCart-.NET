using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Context;
using ShoppingCart.Models;

namespace ShoppingCart.ShoppingCart.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        public async Task<Product> CreateProductAsync(Product product)
        {
            dbContext.Product.Add(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var productToRemove = await dbContext.Product.FindAsync(productId);
            dbContext.Product.Remove(productToRemove);
            return await dbContext.SaveChangesAsync() > 0;

         }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            dbContext.Product.Update(product);
            await dbContext.SaveChangesAsync();
            return product;

    


            // try{
            // dbContext.Product.Update(product);
            // await dbContext.SaveChangesAsync();
            // return product;
            // }
            // catch(Exception ex)
            // {
            //     string msg = ex.Message;
            //     return null;
            // }
          
        }
        public Product GetProduct(int productId)
        {
           return this.dbContext.Product.Find(productId);
           
        }

        public Task<Product> GetProductAsync(int productId)
        {
           //here we did not put async/await because we dont need to await results here, we can await in service Layer
           //or in controller
        return this.dbContext.Product.FindAsync(productId).AsTask();


        }

        public List<Product> GetProducts(int noOfProdducts)
        {
           var products = this.dbContext.Product.OrderByDescending(o=>o.CreatedDate).Take(noOfProdducts).ToList();//this will order the data and then take only specific count and return  
           return products;
        }

        public Task<List<Product>> GetProductsAsync(int noOfProdducts)
        {
        var products = dbContext.Product.OrderByDescending(o=>o.CreatedDate).Take(noOfProdducts).ToListAsync();//this will order the data and then take only specific count and return  
           return products;//this will order the data and then take only specific count and return 
        }

        public Task<Product> GetProductByNameAsync(string name)
        {

            var pro = dbContext.Product;
            return dbContext.Product.FirstOrDefaultAsync(f=>f.Name.ToLower()==name.ToLower());
        }

  
    }
}
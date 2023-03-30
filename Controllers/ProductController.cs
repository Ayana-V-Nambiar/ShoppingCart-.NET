using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingCart.Context;
using ShoppingCart.Models;
using ShoppingCart.ServiceLayer;
using ShoppingCart.ViewModel.Create;
using ShoppingCart.ViewModel.Get;
using ShoppingCart.ViewModel.Update;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ServiceLayer.IProductService productService;

        public ProductController(ServiceLayer.IProductService productService)
        {
            this.productService = productService;
        } 

        [HttpGet("GetProducts")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(List<ProductViewModel>),StatusCodes.Status200OK)]
        
        public async Task<ActionResult> Get()
        {
            var products = await productService.GetProductsAsync();
            var obj = products.Where(a => a.IsDeleted == false);
            var models = obj.Select(product => new ProductViewModel()
            {
                AvailableSince = product.AvailableSince,
                CategoryId = Convert.ToInt16(product.CategoryId),
                Description = product.Description,
                SubCategory = product.SubCategory,
                Gender = product.Gender,
                ProductId = product.ProductId,
                IsActive = product.IsActive,
                IsDeleted = product.IsDeleted,
                ImageURL = product.ImageURL,
                Name = product.Name,
                ProductOwnerId = product.ProductOwnerId,//added
                Price = product.Price
            }).ToList();
            return Ok(models);
        }

        [HttpGet("GetProduct/{id}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(typeof(ProductViewModel),StatusCodes.Status200OK)]
        public async Task<ActionResult> Get(int id)
        {
            var product = await productService.GetProductAsync(id);
            
            if (product == null)
                    return NotFound(new { Message = "Product Not Found"});

            var model = new ProductViewModel()
            {
                AvailableSince = product.AvailableSince,
                CategoryId = Convert.ToInt16(product.CategoryId),
                Description = product.Description,
                ProductId = product.ProductId,
                IsActive = product.IsActive,
                SubCategory = product.SubCategory,
                Gender = product.Gender,
                IsDeleted = product.IsDeleted,
                ImageURL = product.ImageURL,
                Name = product.Name,
                Price = product.Price
            };
            return Ok(model);
           
        }

        [HttpPost("CreateProduct")]
        // [ProducesResponseType(StatusCodes.Status201Created)]
        // [ProducesResponseType(typeof(ModelStateDictionary),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateProduct createProduct)
        {

            //Validation
          var products = await productService.GetProductsAsync();
          var val= products.Where(a => a.Name == createProduct.Name || a.Description == createProduct.Description);
           if(products.FirstOrDefault(a => a.Name == createProduct.Name || a.Description == createProduct.Description) != null)
          {
            return BadRequest(new { Message = "Product Already Exist"});
          }


            var entityToAdd = new Product() //alternative is we can use automapper instead
            {
                Name = createProduct.Name, 
                AvailableSince = createProduct.AvailableSince, 
                CategoryId = createProduct.CategoryId, 
                CreatedDate = DateTime.Now, 
                Description = createProduct.Description, 
                IsActive = createProduct.IsActive,
                IsDeleted = createProduct.IsDeleted,
                SubCategory = createProduct.SubCategory,
                Gender = createProduct.Gender,
                ImageURL = createProduct.ImageURL,
                ProductOwnerId = createProduct.ProductOwnerId,
                Price = createProduct.Price
            };

            entityToAdd.ProductOwner = new ProductOwner()
            {
                OwnerADObjectID = "Admin",
                OwnerName = "Admin"
            };
            var validation = await productService.IsProductNameExistAsync(entityToAdd.Name);
            var createdProduct = await productService.CreateProductAsync(entityToAdd);
            return new CreatedAtRouteResult("Get", new { id = createdProduct.ProductId });
        }
        [HttpPut("UpdateProduct/{id}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(ModelStateDictionary),StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Put(int id, [FromBody] UpdateProduct updateProduct)
        {
             var entityToUpdate = await productService.GetProductAsync(updateProduct.ProductId);
            
            
                // Id = updateProduct.Id,
                entityToUpdate.Name = updateProduct.Name;
                entityToUpdate.AvailableSince = updateProduct.AvailableSince; 
                entityToUpdate.CategoryId = updateProduct.CategoryId;
                entityToUpdate.ModifiedDate = DateTime.Now;
                entityToUpdate.ModifiedBy = "Admin";
                entityToUpdate.Description = updateProduct.Description; 
                entityToUpdate.IsActive = updateProduct.IsActive;
                entityToUpdate.IsDeleted = updateProduct.IsDeleted;
                entityToUpdate.SubCategory = updateProduct.SubCategory;
                entityToUpdate.Gender = updateProduct.Gender;
                entityToUpdate.ImageURL = updateProduct.ImageURL;
                entityToUpdate.Price = updateProduct.Price;
            

            
            var updatedProduct = await productService.UpdateProductAsync(entityToUpdate);
            return Ok();
        
        }

        [HttpDelete("{id}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        { 
            var product = await productService.GetProductAsync(id);
            if (product == null)
                    return NotFound(new { Message = "Product Not Found"});
            product.IsDeleted = true;

            
            var updatedProduct = await productService.UpdateProductAsync(product);
            return Ok();
        }

     


    }
}
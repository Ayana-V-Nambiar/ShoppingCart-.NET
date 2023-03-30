using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Context;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("GetAllCategory")]
        public async Task<ActionResult> Get()
        {
                var categories = await dbContext.Category.ToListAsync();
                return Ok(categories);
        }

        [HttpGet("GetByID/{id}")]  
        public async Task<ActionResult> Get(short id)
        {
            var category = await dbContext.Category.FindAsync(id);
            if(category == null)
            {
                 return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("AddCategory")]//To add categories
         public async Task<ActionResult> Post([FromBody] string value)
         {
           
          var categories = await dbContext.Category.ToListAsync();
          var val= categories.Where(a => a.Name == value);
          if(categories.FirstOrDefault(a => a.Name == value) != null)
          {
            return BadRequest(new { Message = "Category Already Exist"});
          }

            
            var EntityToAdd = new Category() { Name = value, IsActive = true };
            await dbContext.Category.AddAsync(EntityToAdd);
            await dbContext.SaveChangesAsync();
             return new CreatedResult("Get",EntityToAdd.Id);
            

         }
   

        [HttpPut("UpdateById/{id}")]//To update more than one property category(of an entity)
        public async Task<ActionResult> Put(short id, [FromBody] string value)
        {
            var entityInDb = await dbContext.Category.FindAsync(id);
            if (entityInDb == null)
            {
                return BadRequest(new { Message = "Id not found"});
            }
            entityInDb.Name = value;
            dbContext.Update(entityInDb);
            dbContext.SaveChanges();
            return Ok(new { Message = "Updated"});
        }

        [HttpDelete("DeleteById/{id}")]
        public async Task<ActionResult> Delete(short id)
        {
            var entityInDb = await dbContext.Category.FindAsync(id);
            if (entityInDb == null)
            {
                return BadRequest(new { Message = "Id not found"});
            }
            dbContext.Remove(entityInDb);
            dbContext.SaveChanges();
            return Ok(new { Message = "Deleted"});

        }
        
    }
} 
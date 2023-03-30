using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    public class CartController : ControllerBase
    {
        private readonly ICartItemService cartItemService;
        public ILogger<CartController> Logger { get; }

        public CartController(ILogger<CartController> logger,ICartItemService cartItemService)
       {
            Logger = logger;
            this.cartItemService = cartItemService;
        }
        
        [HttpGet ("GetCartItems")]
        public async Task<ActionResult> Get()
        {
            // var adObjName = "Admin";
            // Logger.LogInformation($"Executing {nameof(GetCartItemsAsync)}");
            var cartItems = await cartItemService.GetCartItemsAsync();
            var cartItemsViewModel = cartItems.Select(s => new CartViewModel()
            {
                OwnerADObjectId = s.OwnerADObjectId,
                ProductId = Convert.ToInt32(s.ProductId),
                Id = s.Id,
                UserId = s.UserId,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                ImageURL = s.ImageURL,
                SubCategory = s.SubCategory,
                IsDeleted = s.IsDeleted

            }).ToList();
            return Ok(cartItemsViewModel);
        }

        [HttpGet ("{UserId}")]
        public async Task<ActionResult> Get1([FromRoute] int UserId)
        {
         
            var cartItems = await cartItemService.GetCartItemsAsync();
            var val = cartItems.Where(c => c.UserId == UserId && c.IsDeleted == false);
            var cartItemsViewModel = val.Select(s => new CartViewModel()
            {
                OwnerADObjectId = s.OwnerADObjectId,
                ProductId = Convert.ToInt32(s.ProductId),
                UserId = s.UserId,
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                ImageURL = s.ImageURL,
                SubCategory = s.SubCategory,
                IsDeleted = s.IsDeleted

            }).ToList();
            return Ok(cartItemsViewModel);
        }


        [HttpPost ("CreateCart/{UserId}")]

        public async Task<IActionResult> PostCartAsync([FromBody] CreateCart createCart,[FromRoute] int UserId)
        {
             var cartItems = await cartItemService.GetCartItemsAsync();
            //  var cart = Get();
             var val = cartItems.Where(a => a.ProductId == createCart.ProductId && a.UserId == UserId);
              if(cartItems.FirstOrDefault(a => a.ProductId == createCart.ProductId && a.UserId == UserId && a.IsDeleted == false ) != null)
              {
               return BadRequest(new { Message = "Item Already Exist"});
              }
              else{

             Logger.LogInformation($"Executing {nameof(PostCartAsync)}");            
            var CartInDB = await cartItemService.GetCartItemAsync("Admin",
                Convert.ToInt32(createCart.Id));
            if (CartInDB == null)
            {
                var entity = new CartViewModel()
                {
                    OwnerADObjectId = "Admin",
                    ProductId = createCart.ProductId,
                    UserId = createCart.UserId,
                    Name = createCart.Name,
                    Description = createCart.Description,
                    Price = createCart.Price,
                    ImageURL = createCart.ImageURL,
                    SubCategory = createCart.SubCategory,
                    IsDeleted = false
                };

                var isSuccess = await cartItemService.CreateCartItemAsync(entity);
                return new CreatedAtRouteResult("GetCart",
                  new { id = entity.Id });
            }
            return new CreatedAtRouteResult("GetCart",
                   new { id = CartInDB.Id }); 
              }

        }

        [HttpPut("UpdateCart/{UserId}")]
        public async Task<IActionResult> Put(long UserId, [FromBody] UpdateCart updateCart)
        {
            var entityToUpdate = await cartItemService.GetCartAsync(updateCart.UserId);

        
            entityToUpdate.IsDeleted = updateCart.IsDeleted;

            var updatedCart = await cartItemService.UpdateCartAsync(entityToUpdate);
            return Ok();

        }

        // [HttpDelete ("UserId")]
        // public async Task<IActionResult> Delete(long UserId)
        // {
        //    var cartItems = await cartItemService.GetCartItemsAsync();
        //    var val = cartItems.Where(c => c.UserId == UserId).Any<CartViewModel>();
        //    return Ok();
         
        // }
        [HttpDelete ("{CartId}")]
        public async Task<IActionResult> Delete(long CartId)
        {
            
            var entityToUpdate = await cartItemService.GetCartAsync(CartId);
            var cart = await cartItemService.GetCartItemsAsync();
                           //await cartItemService.GetCartAsync(UserId);
            var UserCart = cart.Where(u => u.Id == CartId).FirstOrDefault<CartViewModel>();
          
        
            if (UserCart == null){
                return NotFound(new { Message = "Item Not Found"});
            }
            else {
                var val = cart.Select(c => c.Id == CartId);
               
           
                foreach (var item in val){

                    UserCart.IsDeleted=true;

                }
            }

                           //  var cartItemsViewModel = UserCart.Select(s => new CartViewModel()
                           // {
                           // UserCart.IsDeleted=true;
                           // });
            var updatedCart = cartItemService.UpdateCartAsync(UserCart);
             return Ok(new {Message = "Item Deleted" });
           
            }




        //  [HttpDelete("DeleteById/{id}")]
        // public async Task<ActionResult> Delete(int UserId)
        // {
        //     var entityInDb = await cartItemService.Cart.FindAsync(UserId);
        //     if (entityInDb == null)
        //     {
        //         return BadRequest(new { Message = "Id not found"});
        //     }
        //     cartItemService.Remove(entityInDb);
        //     // cartItemService.SaveChanges();
        //     return Ok(new { Message = "Deleted"});

        // }
        // }

        
    }
}
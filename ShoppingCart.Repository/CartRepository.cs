using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Context;
using ShoppingCart.Models;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.ViewModel.Get;

namespace ShoppingCart.ShoppingCart.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext dbContext;
        public CartRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<CartViewModel>> GetCartItemsAsync(int noOfItems)
        {
            var cartItems = dbContext.Cart.Take(noOfItems).ToListAsync();
            return cartItems;
            //return dbContext.Cart.Where(w => w.OwnerADObjectId == adObjName).ToListAsync();
        }

        public async Task<CartViewModel> CreateCartItemAsync(CartViewModel cart)
        {
            dbContext.Cart.Add(cart);
            await dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> IsCartItemExistAsync(long UserId)
        {
            var entity = await dbContext.Cart.FindAsync(UserId);
            return entity != null;
        }

        public async Task<bool>  DeleteCartItemAsync(long UserId)
        {
            var entityToDelete = await dbContext.Cart.FindAsync(UserId);
            dbContext.Cart.Remove(entityToDelete);
            return await dbContext.SaveChangesAsync()>0;
        }

        public Task<CartViewModel> GetCartItemAsync(string adObjName, int productId)
        {
              return dbContext.Cart.FirstOrDefaultAsync(f => f.ProductId == productId && f.OwnerADObjectId == adObjName);
        }

        public Task<CartViewModel> GetCartAsync(long UserId)
        {
            return dbContext.Cart.FindAsync(UserId).AsTask();
        }

        public CartViewModel GetCart(long UserId)
        {
            return dbContext.Cart.Find(UserId);
        }

        public async Task<CartViewModel> UpdateCartAsync(CartViewModel cartViewModel)
        {
            dbContext.Cart.Update(cartViewModel);
            dbContext.SaveChanges();
            return cartViewModel;
        }

        // Task<CartViewModel> ICartRepository.GetCartAsync(int UserId)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
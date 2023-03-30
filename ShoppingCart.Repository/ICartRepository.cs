using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;
using ShoppingCart.ViewModel.Get;

namespace ShoppingCart.ShoppingCart.Repository
{
    public interface ICartRepository
    
        {
       
        Task<CartViewModel> CreateCartItemAsync(CartViewModel cart);
        Task<bool> IsCartItemExistAsync(long UserId);
        
        Task<bool> DeleteCartItemAsync(long UserId);
        Task<CartViewModel> GetCartItemAsync(string adObjName, int productId);
        Task<List<CartViewModel>> GetCartItemsAsync(int noOfItems);

        CartViewModel GetCart(long UserId);
        Task<CartViewModel> GetCartAsync (long UserId);
        Task<CartViewModel> UpdateCartAsync (CartViewModel cartViewModel);
    }
    
}
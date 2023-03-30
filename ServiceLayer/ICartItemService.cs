using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;
using ShoppingCart.ViewModel.Get;

namespace ShoppingCart.ServiceLayer
{
    public interface ICartItemService
    
        {
        Task<List<CartViewModel>> GetCartItemsAsync(int noOfItems = 100);
        Task<CartViewModel> CreateCartItemAsync(CartViewModel cart);
        Task<bool> IsCartItemExistAsync(long UserId);     
        Task<bool> DeleteCartItemAsync(long UserId);
        Task<CartViewModel> GetCartItemAsync(string adObjName, int productId);
        Task GetCartItemAsync(long id);


        Task<CartViewModel> GetCartAsync(long UserId);
        CartViewModel GetCart(long UserId);
        Task<CartViewModel> UpdateCartAsync (CartViewModel cartViewModel);
        Task UpdateCartAsync(IEnumerable<CartViewModel> cartItemsViewModel);

        // void SaveChanges();
    }
 }

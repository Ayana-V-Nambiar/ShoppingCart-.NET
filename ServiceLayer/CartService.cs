using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;
using ShoppingCart.ShoppingCart.Repository;
using ShoppingCart.ViewModel.Get;

namespace ShoppingCart.ServiceLayer
{
    public class CartService : ICartItemService
    {
        private readonly ICartRepository cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public Task<CartViewModel> CreateCartItemAsync(CartViewModel cart)
        {
            return cartRepository.CreateCartItemAsync(cart);
        }

        public Task<bool> DeleteCartItemAsync(long UserId)
        {
            return cartRepository.DeleteCartItemAsync(UserId);
        }

        public Task<CartViewModel> GetCartItemAsync(string adObjName, int productId)
        {
            return cartRepository.GetCartItemAsync(adObjName,productId);
        }

        // public Task<List<CartViewModel>> GetCartItemsAsync(string adName)
        // {
        //     return cartRepository.GetCartItemsAsync(adName);
        // }

        public Task<bool> IsCartItemExistAsync(long UserId)
        {
            return cartRepository.IsCartItemExistAsync(UserId);
        }

        public Task<List<CartViewModel>> GetCartItemsAsync(int noOfItems)
        {
            return cartRepository.GetCartItemsAsync(noOfItems);
        }

        Task ICartItemService.GetCartItemAsync(long id)
        {
            throw new NotImplementedException();
        }

        public CartViewModel GetCart(long UserId)
        {
            return cartRepository.GetCart(UserId);
        }

        public Task<CartViewModel> GetCartAsync(long UserId)
        {
            return cartRepository.GetCartAsync(UserId);
        }

        public Task<CartViewModel> UpdateCartAsync(CartViewModel cartViewModel)
        {
            return cartRepository.UpdateCartAsync(cartViewModel);
        }

        public Task UpdateCartAsync(IEnumerable<CartViewModel> cartItemsViewModel)
        {
            throw new NotImplementedException();
        }



        // public Task<CartViewModel> GetCartItemAsync(long id)
        // {
        //     return cartRepository.GetCartItemAsync(id);
        // }


    }
}
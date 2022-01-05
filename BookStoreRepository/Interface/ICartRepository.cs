using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface ICartRepository
    {
        IConfiguration Configuration { get; }

        string AddToCart(CartModel cartModel);
        string UpdateCartQuantity(int cartId, int quantity);
        List<CartModel> RetrieveCartDetails(int userId);
        string DeleteCart(int cartId);
    }
}
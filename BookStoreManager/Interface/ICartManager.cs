using BookStoreModel;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface ICartManager
    {
        string AddToCart(CartModel cartModel);
        string UpdateCartQuantity(int cartId, int quantity);
        List<CartModel> RetrieveCartDetails(int userId);
        string DeleteCart(int cartId);
    }
}
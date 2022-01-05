using BookStoreModel;

namespace BookStoreManager.Interface
{
    public interface ICartManager
    {
        string AddToCart(CartModel cartModel);
        string UpdateCartQuantity(int cartId, int quantity);
    }
}
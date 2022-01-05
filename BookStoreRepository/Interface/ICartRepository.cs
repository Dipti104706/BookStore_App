using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository.Interface
{
    public interface ICartRepository
    {
        IConfiguration Configuration { get; }

        string AddToCart(CartModel cartModel);
        string UpdateCartQuantity(int cartId, int quantity);
    }
}
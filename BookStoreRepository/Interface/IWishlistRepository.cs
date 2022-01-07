using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository.Interface
{
    public interface IWishlistRepository
    {
        IConfiguration Configuration { get; }

        string AddWishlist(WishlistModel wishlist);
        string DeleteBookFromWishlist(int wishlistId);
    }
}
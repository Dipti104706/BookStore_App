using BookStoreModel;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface IWishlistManager
    {
        string AddWishlist(WishlistModel wishlist);
        string DeleteBookFromWishlist(int wishlistId);
        List<WishlistModel> RetrieveWishlist(int userId);
    }
}
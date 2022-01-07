using BookStoreModel;

namespace BookStoreManager.Interface
{
    public interface IWishlistManager
    {
        string AddWishlist(WishlistModel wishlist);
        string DeleteBookFromWishlist(int wishlistId);
    }
}
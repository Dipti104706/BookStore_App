using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface IWishlistRepository
    {
        IConfiguration Configuration { get; }

        string AddWishlist(WishlistModel wishlist);
        string DeleteBookFromWishlist(int wishlistId);
        List<WishlistModel> RetrieveWishlist(int userId);
    }
}
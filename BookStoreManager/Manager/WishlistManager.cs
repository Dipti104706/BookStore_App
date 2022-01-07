using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class WishlistManager : IWishlistManager
    {
        private readonly IWishlistRepository wishlistRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class
        /// </summary>
        /// <param name="repository">taking repository as parameter</param>
        public WishlistManager(IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
        }

        /// <summary>
        /// Register method for manager class
        /// </summary>
        /// <param name="userData">passing register model</param>
        /// <returns>Returns string if Registration is successful</returns>
        public string AddWishlist(WishlistModel wishlist)
        {
            try
            {
                return this.wishlistRepository.AddWishlist(wishlist);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository cartRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class
        /// </summary>
        /// <param name="repository">taking repository as parameter</param>
        public CartManager(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        /// <summary>
        /// Register method for manager class
        /// </summary>
        /// <param name="userData">passing register model</param>
        /// <returns>Returns string if Registration is successful</returns>
        public string AddToCart(CartModel cartModel)
        {
            try
            {
                return this.cartRepository.AddToCart(cartModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string UpdateCartQuantity(int cartId, int quantity)
        {
            try
            {
                return this.cartRepository.UpdateCartQuantity(cartId, quantity);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

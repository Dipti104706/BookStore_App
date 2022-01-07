using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository orderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class
        /// </summary>
        /// <param name="repository">taking repository as parameter</param>
        public OrderManager(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        /// <summary>
        /// Register method for manager class
        /// </summary>
        /// <param name="userData">passing register model</param>
        /// <returns>Returns string if Registration is successful</returns>
        public string AddOrder(OrderModel order)
        {
            try
            {
                return this.orderRepository.AddOrder(order);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

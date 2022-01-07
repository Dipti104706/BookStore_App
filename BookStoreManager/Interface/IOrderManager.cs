using BookStoreModel;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface IOrderManager
    {
        string AddOrder(OrderModel order);
        List<OrderModel> RetrieveOrderDetails(int userId);
    }
}
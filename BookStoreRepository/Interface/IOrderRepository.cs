using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface IOrderRepository
    {
        IConfiguration Configuration { get; }

        string AddOrder(OrderModel order);
        List<OrderModel> RetrieveOrderDetails(int userId);
    }
}
using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository.Interface
{
    public interface IOrderRepository
    {
        IConfiguration Configuration { get; }

        string AddOrder(OrderModel order);
    }
}
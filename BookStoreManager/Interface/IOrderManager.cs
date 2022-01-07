using BookStoreModel;

namespace BookStoreManager.Interface
{
    public interface IOrderManager
    {
        string AddOrder(OrderModel order);
    }
}
using BookStoreModel;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface IAddressManager
    {
        string AddAddress(AddressModel address);
        string UpdateAddress(AddressModel address);
        List<AddressModel> GetAllAddresses();
    }
}
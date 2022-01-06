using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface IAddressRepository
    {
        IConfiguration Configuration { get; }

        string AddAddress(AddressModel address);
        string UpdateAddress(AddressModel address);
        List<AddressModel> GetAllAddresses();
    }
}
using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository.Interface
{
    public interface IAddressRepository
    {
        IConfiguration Configuration { get; }

        string AddAddress(AddressModel address);
    }
}
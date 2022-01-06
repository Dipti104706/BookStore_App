using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository addressRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class
        /// </summary>
        /// <param name="repository">taking repository as parameter</param>
        public AddressManager(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        /// <summary>
        /// Register method for manager class
        /// </summary>
        /// <param name="userData">passing register model</param>
        /// <returns>Returns string if Registration is successful</returns>
        public string AddAddress(AddressModel address)
        {
            try
            {
                return this.addressRepository.AddAddress(address);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

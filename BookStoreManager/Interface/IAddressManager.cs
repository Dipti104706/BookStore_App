﻿using BookStoreModel;

namespace BookStoreManager.Interface
{
    public interface IAddressManager
    {
        string AddAddress(AddressModel address);
        string UpdateAddress(AddressModel address);
    }
}
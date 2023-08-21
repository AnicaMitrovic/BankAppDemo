﻿using BankApp.Domain.Models;

namespace BankApp.Core.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetCustomerList();
    }
}

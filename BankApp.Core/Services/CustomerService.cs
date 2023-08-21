using BankApp.Core.Interfaces;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;

namespace BankApp.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _repo;

        public CustomerService(ICustomerRepo repo)
        {
            _repo=repo;
        }

        //Business logic for Customer
        public List<Customer> GetCustomerList()
        {
            return _repo.GetCustomerList();
        }
    }
}

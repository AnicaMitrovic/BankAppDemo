using BankApp.Core.Interfaces;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;

namespace BankApp.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _repo;

        public CustomerService(ICustomerRepo repo)
        {
            _repo=repo;
        }

        public async Task<ServiceResponse<List<Customer>>> AddNewCustomer(CustomerCreateDto customer)
        {
            return await _repo.AddNewCustomer(customer);
        }

        public async Task<ServiceResponse<List<Customer>>> GetCustomerList()
        {
            return await _repo.GetCustomerList();
        }
    }
}

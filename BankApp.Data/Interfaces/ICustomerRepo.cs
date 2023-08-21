using BankApp.Domain.Models;

namespace BankApp.Data.Interfaces
{
    public interface ICustomerRepo
    {
        List<Customer> GetCustomerList();
    }
}

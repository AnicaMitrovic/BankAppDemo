using BankApp.Data.DataModels;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;

namespace BankApp.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly BankAppDBContext _db;

        public CustomerRepo(BankAppDBContext db)
        {
            _db = db;
        }

        public List<Customer> GetCustomerList()
        {
            return _db.Customers.ToList();
        }
    }
}

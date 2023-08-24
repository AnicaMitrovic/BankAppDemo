using AutoMapper;
using BankApp.Data.DataModels;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.Repos
{
    public class BankAccountRepo : IBankAccountRepo
    {
        private readonly BankAppDBContext _db;
        private readonly IMapper _mapper;

        public BankAccountRepo(BankAppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetBankAccountsResponseDto>>> AddNewBankAccount(BankAccountCreateDto newBankAccount, int customerId)
        {
            var response = new ServiceResponse<List<GetBankAccountsResponseDto>>();
            var bankAccount = _mapper.Map<BankAccount>(newBankAccount);
            bankAccount.Customer = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);

            _db.BankAccounts.Add(bankAccount);
            await _db.SaveChangesAsync();

            response.Data =
                await _db.BankAccounts
                    .Where(ac => ac.CustomerId == customerId)
                    .Select(c => _mapper.Map<GetBankAccountsResponseDto>(c))
                    .ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetBankAccountsResponseDto>>> GetBankAccountsByCustomerId(int customerId)
        {
            var response = new ServiceResponse<List<GetBankAccountsResponseDto>>();
            var dbBankAccounts = await _db.BankAccounts
                .Include(ac => ac.Customer)
                .Where(ac => ac.CustomerId == customerId).ToListAsync(); //only receive the accounts that belong to the logged in Customer

            response.Data = dbBankAccounts.Select(acc => _mapper.Map<GetBankAccountsResponseDto>(acc)).ToList();
            //response.Data = dbBankAccounts.ToList();
            return response;
        }
    }
}

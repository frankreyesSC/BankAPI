using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services
{
    public class AccountService
    {
        private readonly BankContext _context;

        public AccountService(BankContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountDtoOut>> GetAll()
        {

            return await _context.Accounts.Select(x => new AccountDtoOut
            {
                Id = x.Id,
                AccountName = x.AccountTypeNavigation.Name,
                ClientName = x.Client.Name ?? "",
                RegDate = x.RegDate,
                Balance = x.Balance
            }).ToListAsync();
        }

        public async Task<Account?> GetById(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account> Create(Account newAccount)
        {
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();
            return newAccount;
        }

        public async Task UpdateAccount(Account account)
        {
            var existingAccount = await GetById(account.Id);

            if (existingAccount is not null)
            {
                existingAccount.AccountType = account.AccountType;
                existingAccount.ClientId = account.ClientId;
                existingAccount.Balance = account.Balance;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var existingAccount = await GetById(id);

            if (existingAccount is not null)
            {
                _context.Accounts.Remove(existingAccount);
                await _context.SaveChangesAsync();
            }
        }
    }
}

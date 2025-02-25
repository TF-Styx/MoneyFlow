using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ContextMF _context;

        public AccountRepository(ContextMF context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(int? numberAccount, int idUser, BankDomain bankDomain, AccountTypeDomain accountTypeDomain, decimal? balance)
        {
            var accountEntity = new Account
            {
                NumberAccount = numberAccount,
                IdUser = idUser,
                IdBank = bankDomain.IdBank,
                IdAccountType = accountTypeDomain.IdAccountType,
                Balance = balance
            };

            await _context.AddAsync(accountEntity);
            await _context.SaveChangesAsync();

            return _context.Accounts.Where(x => x.IdUser == idUser)
                                    .OrderByDescending(x => x.IdAccount)
                                    .FirstOrDefault(x => x.NumberAccount == numberAccount).IdAccount;
        }
        public int Create(int? numberAccount, int idUser, BankDomain bankDomain, AccountTypeDomain accountTypeDomain, decimal? balance)
        {
            var accountEntity = new Account
            {
                NumberAccount = numberAccount,
                IdUser = idUser,
                IdBank = bankDomain.IdBank,
                IdAccountType = accountTypeDomain.IdAccountType,
                Balance = balance
            };

            _context.Add(accountEntity);
            _context.SaveChanges();

            return _context.Accounts.Where(x => x.IdUser == idUser)
                                    .OrderByDescending(x => x.IdAccount)
                                    .FirstOrDefault(x => x.NumberAccount == numberAccount).IdAccount;
        }

        public async Task<List<AccountDomain>> GetAllAsync()
        {
            var accountList = new List<AccountDomain>();
            var banks = await _context.Banks.ToListAsync();
            var accountsType = await _context.AccountTypes.ToListAsync();
            var accountEntity = await _context.Accounts.ToListAsync();

            foreach (var item in accountEntity)
            {
                var bank = banks.FirstOrDefault(x => x.IdBank == item.IdBank);
                var accountType = accountsType.FirstOrDefault(x => x.IdAccountType == item.IdAccountType);

                accountList.Add(AccountDomain.Create(item.IdAccount, item.NumberAccount, 
                                BankDomain.Create(bank.IdBank, bank.BankName).BankDomain, 
                                AccountTypeDomain.Create(accountType.IdAccountType, accountType.AccountTypeName).AccountTypeDomain, 
                                item.Balance).AccountDomain);
            }

            return accountList;
        }
        public List<AccountDomain> GetAll()
        {
            var accountList = new List<AccountDomain>();
            var banks = _context.Banks.ToList();
            var accountsType = _context.AccountTypes.ToList();
            var accountEntity = _context.Accounts.ToList();

            foreach (var item in accountEntity)
            {
                var bank = banks.FirstOrDefault(x => x.IdBank == item.IdBank);
                var accountType = accountsType.FirstOrDefault(x => x.IdAccountType == item.IdAccountType);

                accountList.Add(AccountDomain.Create(item.IdAccount, item.NumberAccount,
                                BankDomain.Create(bank.IdBank, bank.BankName).BankDomain,
                                AccountTypeDomain.Create(accountType.IdAccountType, accountType.AccountTypeName).AccountTypeDomain,
                                item.Balance).AccountDomain);
            }

            return accountList;
        }

        public async Task<AccountDomain> GetIdAsync(int idAccount)
        {
            var accountEntity = await _context.Accounts.FirstOrDefaultAsync(x => x.IdAccount == idAccount);
            var bank = await _context.Banks.FirstOrDefaultAsync(x => x.IdBank == accountEntity.IdBank);
            var accountType = await _context.AccountTypes.FirstOrDefaultAsync(x => x.IdAccountType == accountEntity.IdAccountType);
            var accountDomain = AccountDomain.Create(accountEntity.IdAccount, accountEntity.NumberAccount, 
                                BankDomain.Create(bank.IdBank, bank.BankName).BankDomain,
                                AccountTypeDomain.Create(accountType.IdAccountType, accountType.AccountTypeName).AccountTypeDomain, 
                                accountEntity.Balance).AccountDomain;

            return accountDomain;
        }
        public AccountDomain GetId(int idAccount)
        {
            var accountEntity = _context.Accounts.FirstOrDefault(x => x.IdAccount == idAccount);
            var bank = _context.Banks.FirstOrDefault(x => x.IdBank == accountEntity.IdBank);
            var accountType = _context.AccountTypes.FirstOrDefault(x => x.IdAccountType == accountEntity.IdAccountType);
            var accountDomain = AccountDomain.Create(accountEntity.IdAccount, accountEntity.NumberAccount,
                                BankDomain.Create(bank.IdBank, bank.BankName).BankDomain,
                                AccountTypeDomain.Create(accountType.IdAccountType, accountType.AccountTypeName).AccountTypeDomain,
                                accountEntity.Balance).AccountDomain;

            return accountDomain;
        }

        public async Task<AccountDomain> GetNumberAsync(int? numberAccount)
        {
            var accountEntity = await _context.Accounts.FirstOrDefaultAsync(x => x.NumberAccount == numberAccount);
            var bank = await _context.Banks.FirstOrDefaultAsync(x => x.IdBank == accountEntity.IdBank);
            var accountType = await _context.AccountTypes.FirstOrDefaultAsync(x => x.IdAccountType == accountEntity.IdAccountType);
            var accountDomain = AccountDomain.Create(accountEntity.IdAccount, accountEntity.NumberAccount,
                                BankDomain.Create(bank.IdBank, bank.BankName).BankDomain,
                                AccountTypeDomain.Create(accountType.IdAccountType, accountType.AccountTypeName).AccountTypeDomain,
                                accountEntity.Balance).AccountDomain;

            return accountDomain;
        }
        public AccountDomain GetNumber(int? numberAccount)
        {
            var accountEntity = _context.Accounts.FirstOrDefault(x => x.NumberAccount == numberAccount);
            var bank = _context.Banks.FirstOrDefault(x => x.IdBank == accountEntity.IdBank);
            var accountType = _context.AccountTypes.FirstOrDefault(x => x.IdAccountType == accountEntity.IdAccountType);
            var accountDomain = AccountDomain.Create(accountEntity.IdAccount, accountEntity.NumberAccount,
                                BankDomain.Create(bank.IdBank, bank.BankName).BankDomain,
                                AccountTypeDomain.Create(accountType.IdAccountType, accountType.AccountTypeName).AccountTypeDomain,
                                accountEntity.Balance).AccountDomain;

            return accountDomain;
        }

        public async Task<int> UpdateAsync(int idAccount, int? numberAccount, BankDomain bankDomain, AccountTypeDomain accountTypeDomain, decimal? balance)
        {
            var entity = await _context.Accounts.FirstOrDefaultAsync(x => x.IdAccount == idAccount);

            entity.NumberAccount = numberAccount;
            entity.IdBank = bankDomain.IdBank;
            entity.IdAccountType = accountTypeDomain.IdAccountType;
            entity.Balance = balance;

            _context.Accounts.Update(entity);
            _context.SaveChanges();

            return idAccount;
        }
        public int Update(int idAccount, int? numberAccount, BankDomain bankDomain, AccountTypeDomain accountTypeDomain, decimal? balance)
        {
            var entity = _context.Accounts.FirstOrDefault(x => x.IdAccount == idAccount);

            entity.NumberAccount = numberAccount;
            entity.IdBank = bankDomain.IdBank;
            entity.IdAccountType = accountTypeDomain.IdAccountType;
            entity.Balance = balance;

            _context.Accounts.Update(entity);
            _context.SaveChanges();

            return idAccount;
        }

        public async Task DeleteAsync(int idAccounts)
        {
            await _context.Accounts.Where(x => x.IdAccount == idAccounts).ExecuteDeleteAsync();
        }
        public void Delete(int idAccounts)
        {
            _context.Accounts.Where(x => x.IdAccount == idAccounts).ExecuteDeleteAsync();
        }
    }
}

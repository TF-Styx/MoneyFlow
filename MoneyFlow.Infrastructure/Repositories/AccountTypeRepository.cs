using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly ContextMF _context;

        public AccountTypeRepository(ContextMF context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string accountTypeName)
        {
            var accountTypeEntity = new AccountType
            {
                AccountTypeName = accountTypeName,
            };

            await _context.AddAsync(accountTypeEntity);
            await _context.SaveChangesAsync();

            return _context.AccountTypes.FirstOrDefault(x => x.AccountTypeName == accountTypeName).IdAccountType;
        }
        public int Create(string accountTypeName)
        {
            var accountTypeEntity = new AccountType
            {
                AccountTypeName = accountTypeName
            };

            _context.AddAsync(accountTypeEntity);
            _context.SaveChangesAsync();

            return _context.AccountTypes.FirstOrDefault(x => x.AccountTypeName == accountTypeName).IdAccountType;
        }

        public async Task<List<AccountTypeDomain>> GetAllAsync()
        {
            var accountTypeList = new List<AccountTypeDomain>();
            var accountTypeEntities = await _context.AccountTypes.ToListAsync();

            foreach (var item in accountTypeEntities)
            {
                accountTypeList.Add(new AccountTypeDomain()
                {
                    IdAccountType = item.IdAccountType,
                    AccountTypeName = item.AccountTypeName,
                });
            }

            return accountTypeList;
        }
        public List<AccountTypeDomain> GetAll()
        {
            var accountTypeList = new List<AccountTypeDomain>();
            var accountTypeEntities = _context.AccountTypes.ToList();

            foreach (var item in accountTypeEntities)
            {
                accountTypeList.Add(new AccountTypeDomain()
                {
                    IdAccountType = item.IdAccountType,
                    AccountTypeName = item.AccountTypeName,
                });
            }

            return accountTypeList;
        }

        public async Task<AccountTypeDomain> GetAsync(int idAccountType)
        {
            var accountTypeEntity = await _context.AccountTypes.FirstOrDefaultAsync(x => x.IdAccountType == idAccountType);
            var accountTypeDomain = new AccountTypeDomain()
            {
                IdAccountType = accountTypeEntity.IdAccountType,
                AccountTypeName = accountTypeEntity.AccountTypeName,
            };

            return accountTypeDomain;
        }
        public AccountTypeDomain Get(int idAccountType)
        {
            var accountTypeEntity = _context.AccountTypes.FirstOrDefault(x => x.IdAccountType == idAccountType);
            var accountTypeDomain = new AccountTypeDomain()
            {
                IdAccountType = accountTypeEntity.IdAccountType,
                AccountTypeName = accountTypeEntity.AccountTypeName,
            };

            return accountTypeDomain;
        }

        public async Task<AccountTypeDomain> GetAsync(string accountTypeName)
        {
            var accountTypeEntity = await _context.AccountTypes.FirstOrDefaultAsync(x => x.AccountTypeName == accountTypeName);

            if (accountTypeEntity == null) { return null; }

            var accountTypeDomain = new AccountTypeDomain()
            {
                IdAccountType = accountTypeEntity.IdAccountType,
                AccountTypeName = accountTypeEntity.AccountTypeName,
            };

            return accountTypeDomain;
        }
        public AccountTypeDomain Get(string accountTypeName)
        {
            var accountTypeEntity = _context.AccountTypes.FirstOrDefault(x => x.AccountTypeName == accountTypeName);

            if (accountTypeEntity == null) { return null; }

            var accountTypeDomain = new AccountTypeDomain()
            {
                IdAccountType = accountTypeEntity.IdAccountType,
                AccountTypeName = accountTypeEntity.AccountTypeName,
            };

            return accountTypeDomain;
        }

        public async Task<int> UpdateAsync(int idAccountType, string accountTypeName)
        {
            var entity = await _context.AccountTypes.FirstOrDefaultAsync(x => x.IdAccountType == idAccountType);
            entity.AccountTypeName = accountTypeName;

            _context.AccountTypes.Update(entity);
            _context.SaveChanges();

            return idAccountType;
        }
        public int Update(int idAccountType, string accountTypeName)
        {
            var entity = _context.AccountTypes.FirstOrDefault(x => x.IdAccountType == idAccountType);
            entity.AccountTypeName = accountTypeName;

            _context.AccountTypes.Update(entity);
            _context.SaveChanges();

            return idAccountType;
        }

        public async Task DeleteAsync(int idAccountType)
        {
            await _context.AccountTypes.Where(x => x.IdAccountType == idAccountType).ExecuteDeleteAsync();
        }
        public void Delete(int idAccountType)
        {
            _context.AccountTypes.Where(x => x.IdAccountType == idAccountType).ExecuteDeleteAsync();
        }
    }
}

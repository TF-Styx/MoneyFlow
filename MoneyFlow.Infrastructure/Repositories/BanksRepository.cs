using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class BanksRepository : IBanksRepository
    {
        private readonly ContextMF _context;
        private readonly Func<ContextMF> _factory;

        public BanksRepository(ContextMF context, Func<ContextMF> factory)
        {
            _context = context;
            _factory = factory;
        }

        public async Task<int> CreateAsync(string bankName)
        {
            var bankEntity = new Bank
            {
                BankName = bankName
            };

            await _context.AddAsync(bankEntity);
            await _context.SaveChangesAsync();

            return _context.Banks.FirstOrDefault(x => x.BankName == bankName).IdBank;
        }
        public int Create(string bankName)
        {
            var bankEntity = new Bank
            {
                BankName = bankName
            };

            _context.AddAsync(bankEntity);
            _context.SaveChangesAsync();

            return _context.Banks.FirstOrDefault(x => x.BankName == bankName).IdBank;
        }

        //public async Task<List<BankDomain>> GetAllAsync()
        //{
        //    var bankList = new List<BankDomain>();
        //    var bankEntities = await _context.Banks.ToListAsync();

        //    foreach (var item in bankEntities)
        //    {
        //        bankList.Add(BankDomain.Create(item.IdBank, item.BankName).BankDomain);
        //    }

        //    return bankList;
        //}
        public async Task<List<BankDomain>> GetAllAsync()
        {
            using (var context = _factory())
            {
                var bankList = new List<BankDomain>();
                var bankEntities = await context.Banks.ToListAsync();

                foreach (var item in bankEntities)
                {
                    bankList.Add(BankDomain.Create(item.IdBank, item.BankName).BankDomain);
                }

                return bankList;
            }
        }
        public List<BankDomain> GetAll()
        {
            var bankList = new List<BankDomain>();
            var bankEntities = _context.Banks.ToList();

            foreach (var item in bankEntities)
            {
                bankList.Add(BankDomain.Create(item.IdBank, item.BankName).BankDomain);
            }

            return bankList;
        }

        public async Task<BankDomain> GetAsync(int idBank)
        {
            var bankEntity = await _context.Banks.FirstOrDefaultAsync(x => x.IdBank == idBank);
            var bankDomain = BankDomain.Create(idBank, bankEntity.BankName).BankDomain;

            return bankDomain;
        }
        public BankDomain Get(int idBank)
        {
            var bankEntity = _context.Banks.FirstOrDefault(x => x.IdBank == idBank);
            var bankDomain = BankDomain.Create(idBank, bankEntity.BankName).BankDomain;

            return bankDomain;
        }

        public async Task<BankDomain> GetAsync(string bankName)
        {
            var bankEntity = await _context.Banks.FirstOrDefaultAsync(x => x.BankName == bankName);

            if (bankEntity == null) { return null; }

            var bankDomain = BankDomain.Create(bankEntity.IdBank, bankName).BankDomain;

            return bankDomain;
        }
        public BankDomain Get(string bankName)
        {
            var bankEntity = _context.Banks.FirstOrDefault(x => x.BankName == bankName);

            if (bankEntity == null) { return null; }

            var bankDomain = BankDomain.Create(bankEntity.IdBank, bankName).BankDomain;

            return bankDomain;
        }

        //public async Task<UserBanksDomain> GetByIdUserAsync(int idUser)
        //{
        //    var banksId = await _context.Accounts.Where(x => x.IdUser == idUser).Select(x => x.IdBank).Distinct().ToListAsync();
        //    var userBanks = await _context.Banks.Where(x => banksId.Contains(x.IdBank)).ToListAsync();
        //    var bankDomain = new List<BankDomain>();

        //    foreach (var item in userBanks)
        //    {
        //        bankDomain.Add(BankDomain.Create(item.IdBank, item.BankName).BankDomain);
        //    }

        //    var domain = new UserBanksDomain()
        //    {
        //        IdUser = idUser,
        //        Banks = bankDomain
        //    };

        //    return domain;
        //}
        public async Task<UserBanksDomain> GetByIdUserAsync(int idUser)
        {
            using (var context = _factory())
            {
                var banksId = await context.Accounts.Where(x => x.IdUser == idUser).Select(x => x.IdBank).Distinct().ToListAsync();
                var userBanks = await context.Banks.Where(x => banksId.Contains(x.IdBank)).ToListAsync();
                var bankDomain = new List<BankDomain>();

                foreach (var item in userBanks)
                {
                    bankDomain.Add(BankDomain.Create(item.IdBank, item.BankName).BankDomain);
                }

                var domain = new UserBanksDomain()
                {
                    IdUser = idUser,
                    Banks = bankDomain
                };

                return domain;
            }
        }        
        public UserBanksDomain GetByIdUser(int idUser)
        {
            var banksId = _context.Accounts.Where(x => x.IdUser == idUser).Select(x => x.IdBank).Distinct().ToList();
            var userBanks = _context.Banks.Where(x => banksId.Contains(x.IdBank)).ToList();
            var bankDomain = new List<BankDomain>();

            foreach (var item in userBanks)
            {
                bankDomain.Add(BankDomain.Create(item.IdBank, item.BankName).BankDomain);
            }

            var domain = new UserBanksDomain()
            {
                IdUser = idUser,
                Banks = bankDomain
            };

            return domain;
        }

        public async Task<int> UpdateAsync(int idBank, string bankName)
        {
            var entity = await _context.Banks.FirstOrDefaultAsync(x => x.IdBank == idBank);
            entity.BankName = bankName;

            _context.Banks.Update(entity);
            _context.SaveChanges();

            return idBank;
        }
        public int Update(int idBank, string bankName)
        {
            var entity = _context.Banks.FirstOrDefault(x => x.IdBank == idBank);
            entity.BankName = bankName;

            _context.Banks.Update(entity);
            _context.SaveChanges();

            return idBank;
        }

        public async Task DeleteAsync(int idBank)
        {
            await _context.Banks.Where(x => x.IdBank == idBank).ExecuteDeleteAsync();
        }
        public void Delete(int idBank)
        {
            _context.Banks.Where(x => x.IdBank == idBank).ExecuteDelete();
        }
    }
}

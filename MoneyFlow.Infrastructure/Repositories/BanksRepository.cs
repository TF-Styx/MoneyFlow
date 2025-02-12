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

        public BanksRepository(ContextMF context)
        {
            _context = context;
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

        public async Task<List<BankDomain>> GetAll()
        {
            var bankList = new List<BankDomain>();
            var bankEntities = await _context.Banks.ToListAsync();

            foreach (var item in bankEntities)
            {
                bankList.Add(new BankDomain()
                {
                    IdBank = item.IdBank,
                    BankName = item.BankName,
                });
            }

            return bankList;
        }

        public async Task<BankDomain> Get(int idBank)
        {
            var bankEntity = await _context.Banks.FirstOrDefaultAsync(x => x.IdBank == idBank);
            var bankDomain = new BankDomain()
            {
                IdBank = bankEntity.IdBank,
                BankName = bankEntity.BankName,
            };

            return bankDomain;
        }

        public async Task<BankDomain> Get(string bankName)
        {
            var bankEntity = await _context.Banks.FirstOrDefaultAsync(x => x.BankName == bankName);
            var bankDomain = new BankDomain()
            {
                IdBank = bankEntity.IdBank,
                BankName = bankEntity.BankName,
            };

            return bankDomain;
        }

        public async Task<int> Update(int idBank, string bankName)
        {
            await _context.Banks
                .Where(x => x.IdBank == idBank)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(x => x.BankName, x => bankName));

            return idBank;
        }

        public async Task Delete(int idBank)
        {
            await _context.Banks.Where(x => x.IdBank == idBank).ExecuteDeleteAsync();
        }
    }
}

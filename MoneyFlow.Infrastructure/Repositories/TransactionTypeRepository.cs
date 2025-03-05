using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly ContextMF _context;

        public TransactionTypeRepository(ContextMF context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string? transactionTypeName, string? description)
        {
            var transactionTypeEntity = new TransactionType()
            {
                TransactionTypeName = transactionTypeName,
                Description = description
            };

            await _context.AddAsync(transactionTypeEntity);
            await _context.SaveChangesAsync();

            return _context.TransactionTypes.FirstOrDefault(x => x.TransactionTypeName == transactionTypeName).IdTransactionType;
        }
        public int Create(string? transactionTypeName, string? description)
        {
            var transactionTypeEntity = new TransactionType()
            {
                TransactionTypeName = transactionTypeName,
                Description = description
            };

            _context.Add(transactionTypeEntity);
            _context.SaveChanges();

            return _context.TransactionTypes.FirstOrDefault(x => x.TransactionTypeName == transactionTypeName).IdTransactionType;
        }

        public async Task<List<TransactionTypeDomain>> GetAllAsync()
        {
            var transactionTypeList = new List<TransactionTypeDomain>();
            var transactionTypeEntity = await _context.TransactionTypes.ToListAsync();

            foreach (var item in transactionTypeEntity)
            {
                transactionTypeList.Add(TransactionTypeDomain.Create(item.IdTransactionType, item.TransactionTypeName, item.Description).TransactionTypeDomain);
            }

            return transactionTypeList;
        }
        public List<TransactionTypeDomain> GetAll()
        {
            var transactionTypeList = new List<TransactionTypeDomain>();
            var transactionTypeEntity = _context.TransactionTypes.ToList();

            foreach (var item in transactionTypeEntity)
            {
                transactionTypeList.Add(TransactionTypeDomain.Create(item.IdTransactionType, item.TransactionTypeName, item.Description).TransactionTypeDomain);
            }

            return transactionTypeList;
        }

        public async Task<TransactionTypeDomain> GetAsync(int idTransactionType)
        {
            var transactionTypeEntity = await _context.TransactionTypes.FirstOrDefaultAsync(x => x.IdTransactionType == idTransactionType);
            var transactionTypeDomain = TransactionTypeDomain.Create(transactionTypeEntity.IdTransactionType, transactionTypeEntity.TransactionTypeName, transactionTypeEntity.Description).TransactionTypeDomain;

            return transactionTypeDomain;
        }
        public TransactionTypeDomain Get(int idTransactionType)
        {
            var transactionTypeEntity = _context.TransactionTypes.FirstOrDefault(x => x.IdTransactionType == idTransactionType);
            var transactionTypeDomain = TransactionTypeDomain.Create(transactionTypeEntity.IdTransactionType, transactionTypeEntity.TransactionTypeName, transactionTypeEntity.Description).TransactionTypeDomain;

            return transactionTypeDomain;
        }

        public async Task<TransactionTypeDomain> GetAsync(string transactionTypeName)
        {
            var transactionTypeEntity = await _context.TransactionTypes.FirstOrDefaultAsync(x => x.TransactionTypeName == transactionTypeName);

            if (transactionTypeEntity == null) { return null; }

            var transactionTypeDomain = TransactionTypeDomain.Create(transactionTypeEntity.IdTransactionType, transactionTypeEntity.TransactionTypeName, transactionTypeEntity.Description).TransactionTypeDomain;

            return transactionTypeDomain;
        }
        public TransactionTypeDomain Get(string transactionTypeName)
        {
            var transactionTypeEntity = _context.TransactionTypes.FirstOrDefault(x => x.TransactionTypeName == transactionTypeName);

            if (transactionTypeEntity == null) { return null; }

            var transactionTypeDomain = TransactionTypeDomain.Create(transactionTypeEntity.IdTransactionType, transactionTypeEntity.TransactionTypeName, transactionTypeEntity.Description).TransactionTypeDomain;

            return transactionTypeDomain;
        }

        public async Task<int> UpdateAsync(int idTransactionType, string? transactionTypeName, string? description)
        {
            var entity = await _context.TransactionTypes.FirstOrDefaultAsync(x => x.IdTransactionType == idTransactionType);

            entity.TransactionTypeName = transactionTypeName;
            entity.Description = description;

            _context.TransactionTypes.Update(entity);
            _context.SaveChanges();

            return idTransactionType;
        }
        public int Update(int idTransactionType, string? transactionTypeName, string? description)
        {
            var entity = _context.TransactionTypes.FirstOrDefault(x => x.IdTransactionType == idTransactionType);

            entity.TransactionTypeName = transactionTypeName;
            entity.Description = description;

            _context.TransactionTypes.Update(entity);
            _context.SaveChanges();

            return idTransactionType;
        }

        public async Task DeleteAsync(int idTransactionType)
        {
            await _context.TransactionTypes.Where(x => x.IdTransactionType == idTransactionType).ExecuteDeleteAsync();
        }
        public void Delete(int idTransactionType)
        {
            _context.TransactionTypes.Where(x => x.IdTransactionType == idTransactionType).ExecuteDelete();
        }
    }
}

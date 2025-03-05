using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class FinancialRecordRepository : IFinancialRecordRepository
    {
        private readonly ContextMF _context;

        public FinancialRecordRepository(ContextMF context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            var entity = new FinancialRecord()
            {
                RecordName = recordName,
                Ammount = amount,
                Description = description,
                IdTransactionType = idTransactionType,
                IdUser = idUser,
                IdCategory = idCategory,
                IdAccount = idAccount,
                Date = date
            };

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _context.FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == entity.IdFinancialRecord).IdFinancialRecord;
        }
        public int Create(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            var entity = new FinancialRecord()
            {
                RecordName = recordName,
                Ammount = amount,
                Description = description,
                IdTransactionType = idTransactionType,
                IdUser = idUser,
                IdCategory = idCategory,
                IdAccount = idAccount,
                Date = date
            };

            _context.Add(entity);
            _context.SaveChanges();

            return _context.FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == entity.IdFinancialRecord).IdFinancialRecord;
        }

        public async Task<List<FinancialRecordDomain>> GetAllAsync()
        {
            var list = new List<FinancialRecordDomain>();
            var entity = await _context.FinancialRecords.ToListAsync();

            foreach (var item in entity)
            {
                list.Add(FinancialRecordDomain.Create(item.IdFinancialRecord, item.RecordName, item.Ammount, item.Description, item.IdTransactionType, item.IdUser, item.IdCategory, item.IdAccount, item.Date).FinancialRecordDomain);
            }

            return list;
        }
        public List<FinancialRecordDomain> GetAll()
        {
            var list = new List<FinancialRecordDomain>();
            var entity = _context.FinancialRecords.ToList();

            foreach (var item in entity)
            {
                list.Add(FinancialRecordDomain.Create(item.IdFinancialRecord, item.RecordName, item.Ammount, item.Description, item.IdTransactionType, item.IdUser, item.IdCategory, item.IdAccount, item.Date).FinancialRecordDomain);
            }

            return list;
        }

        public async Task<FinancialRecordDomain> GetAsync(int idFinancialRecord)
        {
            var entity = await _context.FinancialRecords.FirstOrDefaultAsync(x => x.IdFinancialRecord == idFinancialRecord);
            var domain = FinancialRecordDomain.Create(entity.IdFinancialRecord, entity.RecordName, entity.Ammount, entity.Description, entity.IdTransactionType, entity.IdUser, entity.IdCategory, entity.IdAccount, entity.Date).FinancialRecordDomain;

            return domain;
        }
        public FinancialRecordDomain Get(int idFinancialRecord)
        {
            var entity = _context.FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == idFinancialRecord);
            var domain = FinancialRecordDomain.Create(entity.IdFinancialRecord, entity.RecordName, entity.Ammount, entity.Description, entity.IdTransactionType, entity.IdUser, entity.IdCategory, entity.IdAccount, entity.Date).FinancialRecordDomain;

            return domain;
        }

        public async Task<FinancialRecordDomain> GetAsync(string recordName)
        {
            var entity = await _context.FinancialRecords.FirstOrDefaultAsync(x => x.RecordName == recordName);
            var domain = FinancialRecordDomain.Create(entity.IdFinancialRecord, entity.RecordName, entity.Ammount, entity.Description, entity.IdTransactionType, entity.IdUser, entity.IdCategory, entity.IdAccount, entity.Date).FinancialRecordDomain;

            return domain;
        }
        public FinancialRecordDomain Get(string recordName)
        {
            var entity = _context.FinancialRecords.FirstOrDefault(x => x.RecordName == recordName);
            var domain = FinancialRecordDomain.Create(entity.IdFinancialRecord, entity.RecordName, entity.Ammount, entity.Description, entity.IdTransactionType, entity.IdUser, entity.IdCategory, entity.IdAccount, entity.Date).FinancialRecordDomain;

            return domain;
        }

        public async Task<int> UpdateAsync(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            var entity = await _context.FinancialRecords.FirstOrDefaultAsync(x => x.IdFinancialRecord == idFinancialRecord);

            entity.RecordName = recordName;
            entity.Ammount = amount;
            entity.Description = description;
            entity.IdTransactionType = idTransactionType;
            entity.IdUser = idUser;
            entity.IdCategory = idCategory;
            entity.IdAccount = idAccount;
            entity.Date = date;

            _context.Update(entity);
            _context.SaveChanges();

            return idFinancialRecord;
        }
        public int Update(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            var entity = _context.FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == idFinancialRecord);

            entity.RecordName = recordName;
            entity.Ammount = amount;
            entity.Description = description;
            entity.IdTransactionType = idTransactionType;
            entity.IdUser = idUser;
            entity.IdCategory = idCategory;
            entity.IdAccount = idAccount;
            entity.Date = date;

            _context.Update(entity);
            _context.SaveChanges();

            return idFinancialRecord;
        }

        public async Task DeleteAsync(int idFinancialRecord)
        {
            await _context.FinancialRecords.Where(x => x.IdFinancialRecord == idFinancialRecord).ExecuteDeleteAsync();
        }
        public void Delete(int idFinancialRecord)
        {
            _context.FinancialRecords.Where(x => x.IdFinancialRecord == idFinancialRecord).ExecuteDelete();
        }
    }
}

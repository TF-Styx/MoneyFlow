using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class FinancialRecordRepository : IFinancialRecordRepository
    {
        private readonly Func<ContextMF> _factory;

        public FinancialRecordRepository(Func<ContextMF> factory)
        {
            _factory = factory;
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<int> CreateAsync(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            using (var context = _factory())
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

                await context.AddAsync(entity);
                await context.SaveChangesAsync();

                return context.FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == entity.IdFinancialRecord).IdFinancialRecord;
            }
        }
        public int Create(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            using (var context = _factory())
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

                context.Add(entity);
                context.SaveChanges();

                return context.FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == entity.IdFinancialRecord).IdFinancialRecord;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<FinancialRecordDomain>> GetAllAsync()
        {
            using (var context = _factory())
            {
                var list = new List<FinancialRecordDomain>();
                var entity = await context.FinancialRecords.ToListAsync();

                foreach (var item in entity)
                {
                    list.Add(FinancialRecordDomain.Create(item.IdFinancialRecord, item.RecordName, item.Ammount, item.Description, item.IdTransactionType, item.IdUser, item.IdCategory, item.IdAccount, item.Date).FinancialRecordDomain);
                }

                return list;
            }
        }
        public List<FinancialRecordDomain> GetAll()
        {
            using (var context = _factory())
            {
                var list = new List<FinancialRecordDomain>();
                var entity = context.FinancialRecords.ToList();

                foreach (var item in entity)
                {
                    list.Add(FinancialRecordDomain.Create(item.IdFinancialRecord, item.RecordName, item.Ammount, item.Description, item.IdTransactionType, item.IdUser, item.IdCategory, item.IdAccount, item.Date).FinancialRecordDomain);
                }

                return list;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<FinancialRecordDomain> GetAsync(int idFinancialRecord)
        {
            using (var context = _factory())
            {
                var entity = await context.FinancialRecords.FirstOrDefaultAsync(x => x.IdFinancialRecord == idFinancialRecord);
                var domain = FinancialRecordDomain.Create(entity.IdFinancialRecord, entity.RecordName, entity.Ammount, entity.Description, entity.IdTransactionType, entity.IdUser, entity.IdCategory, entity.IdAccount, entity.Date).FinancialRecordDomain;

                return domain;
            }
        }
        public FinancialRecordDomain Get(int idFinancialRecord)
        {
            using (var context = _factory())
            {
                var entity = context.FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == idFinancialRecord);
                var domain = FinancialRecordDomain.Create(entity.IdFinancialRecord, entity.RecordName, entity.Ammount, entity.Description, entity.IdTransactionType, entity.IdUser, entity.IdCategory, entity.IdAccount, entity.Date).FinancialRecordDomain;

                return domain;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<FinancialRecordDomain> GetAsync(string recordName)
        {
            using (var context = _factory())
            {
                var entity = await context.FinancialRecords.FirstOrDefaultAsync(x => x.RecordName == recordName);
                var domain = FinancialRecordDomain.Create(entity.IdFinancialRecord, entity.RecordName, entity.Ammount, entity.Description, entity.IdTransactionType, entity.IdUser, entity.IdCategory, entity.IdAccount, entity.Date).FinancialRecordDomain;

                return domain;
            }
        }
        public FinancialRecordDomain Get(string recordName)
        {
            using (var context = _factory())
            {
                var entity = context.FinancialRecords.FirstOrDefault(x => x.RecordName == recordName);
                var domain = FinancialRecordDomain.Create(entity.IdFinancialRecord, entity.RecordName, entity.Ammount, entity.Description, entity.IdTransactionType, entity.IdUser, entity.IdCategory, entity.IdAccount, entity.Date).FinancialRecordDomain;

                return domain;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<int> UpdateAsync(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            using (var context = _factory())
            {
                var entity = await context.FinancialRecords.FirstOrDefaultAsync(x => x.IdFinancialRecord == idFinancialRecord);

                entity.RecordName = recordName;
                entity.Ammount = amount;
                entity.Description = description;
                entity.IdTransactionType = idTransactionType;
                entity.IdUser = idUser;
                entity.IdCategory = idCategory;
                entity.IdAccount = idAccount;
                entity.Date = date;

                context.Update(entity);
                context.SaveChanges();

                return idFinancialRecord;
            }
        }
        public int Update(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            using (var context = _factory())
            {
                var entity = context.FinancialRecords.FirstOrDefault(x => x.IdFinancialRecord == idFinancialRecord);

                entity.RecordName = recordName;
                entity.Ammount = amount;
                entity.Description = description;
                entity.IdTransactionType = idTransactionType;
                entity.IdUser = idUser;
                entity.IdCategory = idCategory;
                entity.IdAccount = idAccount;
                entity.Date = date;

                context.Update(entity);
                context.SaveChanges();

                return idFinancialRecord;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task DeleteAsync(int idFinancialRecord)
        {
            using (var context = _factory())
            {
                await context.FinancialRecords.Where(x => x.IdFinancialRecord == idFinancialRecord).ExecuteDeleteAsync();
            }
        }
        public void Delete(int idFinancialRecord)
        {
            using (var context = _factory())
            {
                context.FinancialRecords.Where(x => x.IdFinancialRecord == idFinancialRecord).ExecuteDelete();
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------
    }
}

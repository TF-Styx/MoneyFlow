using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;
using System.Diagnostics;

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

                return await context.FinancialRecords
                    .Where(x => x.IdUser == idUser)
                    .OrderByDescending(x => x.IdFinancialRecord)
                    .Select(x => x.IdFinancialRecord)
                    .FirstOrDefaultAsync();
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

                return context.FinancialRecords
                    .Where(x => x.IdUser == idUser)
                    .OrderByDescending(x => x.IdFinancialRecord)
                    .Select(x => x.IdFinancialRecord)
                    .FirstOrDefault();
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<FinancialRecordDomain>> GetAllAsync(int idUser)
        {
            using (var context = _factory())
            {
                var list = new List<FinancialRecordDomain>();
                var entity = await context.FinancialRecords.Where(x => x.IdUser == idUser).ToListAsync();

                foreach (var item in entity)
                {
                    list.Add(FinancialRecordDomain.Create(item.IdFinancialRecord, item.RecordName, item.Ammount, item.Description, item.IdTransactionType, item.IdUser, item.IdCategory, item.IdAccount, item.Date).FinancialRecordDomain);
                }

                return list;
            }
        }
        public List<FinancialRecordDomain> GetAll(int idUser)
        {
            using (var context = _factory())
            {
                var list = new List<FinancialRecordDomain>();
                var entity = context.FinancialRecords.Where(x => x.IdUser == idUser).ToList();

                foreach (var item in entity)
                {
                    list.Add(FinancialRecordDomain.Create(item.IdFinancialRecord, item.RecordName, item.Ammount, item.Description, item.IdTransactionType, item.IdUser, item.IdCategory, item.IdAccount, item.Date).FinancialRecordDomain);
                }

                return list;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<FinancialRecordViewingDomain>> GetAllViewingAsync(int idUser)
        {
            using (var context = _factory())
            {
                var list = new List<FinancialRecordViewingDomain>();
                
                var entities = await context.FinancialRecords
                    .Where(x => x.IdUser == idUser)
                        .Include(x => x.IdTransactionTypeNavigation)
                        .Include(x => x.IdCategoryNavigation)
                        .Include(x => x.IdAccountNavigation)
                            .ToListAsync();

                //Debug.WriteLine($"Количество записей {entities.Count}");

                var subcategories = await context.Subcategories.Where(x => x.IdUser == idUser).ToListAsync();
                var catLinkSubs = await context.CatLinkSubs.Where(x => x.IdUser == idUser).ToListAsync();

                foreach (var item in entities)
                {
                    var idSubcategories = catLinkSubs.Where(x => x.IdCategory == item.IdCategory).Select(x => x.IdSubcategory).ToList();
                    var subcategoriesName = subcategories.Where(x => idSubcategories.Contains(x.IdSubcategory)).Select(x => x.SubcategoryName).ToList();

                    list.Add(FinancialRecordViewingDomain
                            .Create
                            (
                                item.IdFinancialRecord, 
                                item.RecordName, 
                                item.Ammount, 
                                item.Description, 
                                item.IdTransactionTypeNavigation.TransactionTypeName, 
                                item.IdUser, 
                                item.IdCategoryNavigation.CategoryName, 
                                subcategoriesName, 
                                item.IdAccountNavigation.NumberAccount, 
                                item.Date
                            ).FinancialRecordViewingDomain);
                }

                return list;
            }
        }
        public List<FinancialRecordViewingDomain> GetAllViewing(int idUser)
        {
            return Task.Run(() => GetAllViewingAsync(idUser)).Result;
        }

        public async Task<FinancialRecordViewingDomain> GetByIdAsync(int idUser, int idFinancialRecord, int idCategory, int idSubcategory)
        {
            using (var context = _factory())
            {
                var subcategorys = await context.CatLinkSubs.Where(x => x.IdUser == idUser && x.IdCategory == idCategory && x.IdSubcategory == idSubcategory)
                                                            .Select(x => x.IdSubcategoryNavigation.SubcategoryName).ToListAsync();

                var entity = await context.FinancialRecords
                    .Include(x => x.IdTransactionTypeNavigation)
                    .Include(x => x.IdUserNavigation)
                    .Include(x => x.IdAccountNavigation)
                    .Include(x => x.IdCategoryNavigation)
                        .FirstOrDefaultAsync(x => x.IdFinancialRecord == idFinancialRecord);

                var domain = FinancialRecordViewingDomain.Create
                    (
                        entity.IdFinancialRecord,
                        entity.RecordName,
                        entity.Ammount,
                        entity.Description,
                        entity.IdTransactionTypeNavigation.TransactionTypeName,
                        idUser,
                        entity.IdCategoryNavigation.CategoryName,
                        subcategorys,
                        entity.IdAccountNavigation.NumberAccount,
                        entity.Date
                    ).FinancialRecordViewingDomain;

                return domain;
            }
        }
        public FinancialRecordViewingDomain GetById(int idUser, int idFinancialRecord, int idCategory, int idSubcategory)
        {
            using (var context = _factory())
            {
                var subcategorys = context.CatLinkSubs.Where(x => x.IdUser == idUser && x.IdCategory == idCategory && x.IdSubcategory == idSubcategory)
                                                      .Select(x => x.IdSubcategoryNavigation.SubcategoryName).ToList();

                var entity = context.FinancialRecords
                    .Include(x => x.IdTransactionTypeNavigation)
                    .Include(x => x.IdUserNavigation)
                    .Include(x => x.IdAccountNavigation)
                    .Include(x => x.IdCategoryNavigation)
                        .FirstOrDefault(x => x.IdFinancialRecord == idFinancialRecord);

                var domain = FinancialRecordViewingDomain.Create
                    (
                        entity.IdFinancialRecord, 
                        entity.RecordName, 
                        entity.Ammount, 
                        entity.Description, 
                        entity.IdTransactionTypeNavigation.TransactionTypeName, 
                        idUser, 
                        entity.IdCategoryNavigation.CategoryName, 
                        subcategorys, 
                        entity.IdAccountNavigation.NumberAccount, 
                        entity.Date
                    ).FinancialRecordViewingDomain;

                return domain;
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

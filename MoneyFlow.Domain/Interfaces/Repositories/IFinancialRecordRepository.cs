using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Domain.Interfaces.Repositories
{
    public interface IFinancialRecordRepository
    {
        Task<int> CreateAsync(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);
        int Create(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);

        Task<List<FinancialRecordDomain>> GetAllAsync(int idUser);
        List<FinancialRecordDomain> GetAll(int idUser);

        Task<List<FinancialRecordViewingDomain>> GetAllViewingAsync(int idUser, FinancialRecordFilterDomain filter);
        List<FinancialRecordViewingDomain> GetAllViewing(int idUser);

        Task<FinancialRecordViewingDomain> GetByIdAsync(int idUser, int idFinancialRecord, int? idCategory, int idSubcategory);
        FinancialRecordViewingDomain GetById(int idUser, int idFinancialRecord, int? idCategory, int idSubcategory);

        Task<FinancialRecordDomain> GetAsync(int idFinancialRecord);
        FinancialRecordDomain Get(int idFinancialRecord);
        
        Task<FinancialRecordDomain> GetAsync(string recordName);
        FinancialRecordDomain Get(string recordName);

        Task<int> UpdateAsync(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);
        int Update(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);

        Task DeleteAsync(int idFinancialRecord);
        void Delete(int idFinancialRecord);
    }
}
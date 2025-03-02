﻿using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Domain.Interfaces.Repositories
{
    public interface IFinancialRecordRepository
    {
        Task<int> CreateAsync(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);
        int Create(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);

        Task<List<FinancialRecordDomain>> GetAllAsync();
        List<FinancialRecordDomain> GetAll();
        
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
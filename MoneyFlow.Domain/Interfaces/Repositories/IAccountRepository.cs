﻿using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<int> CreateAsync(int? numberAccount, int idUser, BankDomain bankDomain, AccountTypeDomain accountTypeDomain, decimal? balance);
        int Create(int? numberAccount, int idUser, BankDomain bankDomain, AccountTypeDomain accountTypeDomain, decimal? balance);

        Task<List<AccountDomain>> GetAllAsync(int idUser);
        List<AccountDomain> GetAll(int idUser);

        Task<AccountDomain> GetIdAsync(int idAccount);
        AccountDomain GetId(int idAccount);

        Task<AccountDomain> GetNumberAsync(int? numberAccount);
        AccountDomain GetNumber(int? numberAccount);

        Task<int> UpdateAsync(int idAccount, int? numberAccount, BankDomain bankDomain, AccountTypeDomain accountTypeDomain, decimal? balance);
        int Update(int idAccount, int? numberAccount, BankDomain bankDomain, AccountTypeDomain accountTypeDomain, decimal? balance);

        Task DeleteAsync(int idAccounts);
        void Delete(int idAccounts);
    }
}
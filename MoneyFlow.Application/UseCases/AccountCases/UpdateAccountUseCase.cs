﻿using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.AccountCaseInterface;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.AccountCases
{
    public class UpdateAccountUseCase : IUpdateAccountUseCase
    {
        private readonly IAccountRepository _accountRepository;

        public UpdateAccountUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<int> UpdateAsyncAccount(int idAccount, int? numberAccount, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance)
        {
            var existAccount = await _accountRepository.GetIdAsync(idAccount);

            if (existAccount == null)
            {
                throw new Exception("Данного пользователя не существует!!");
            }

            return await _accountRepository.UpdateAsync(idAccount, numberAccount, bankDTO.ToDomain().BankDomain, accountTypeDTO.ToDomain().AccountTypeDomain, balance);
        }
        public int UpdateAccount(int idAccount, int? numberAccount, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance)
        {
            var existUser = _accountRepository.GetId(idAccount);

            if (existUser == null)
            {
                throw new Exception("Данного пользователя не существует!!");
            }

            return _accountRepository.Update(idAccount, numberAccount, bankDTO.ToDomain().BankDomain, accountTypeDTO.ToDomain().AccountTypeDomain, balance);
        }
    }
}

using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.AccountCaseInterface;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.AccountCases
{
    public class GetAccountUseCase : IGetAccountUseCase
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<List<AccountDTO>> GetAllAsyncAccount(int idUser)
        {
            var accounts = await _accountRepository.GetAllAsync(idUser);
            var accountsDTO = accounts.ToListDTO();

            return accountsDTO;
        }
        public List<AccountDTO> GetAllAccount(int idUser)
        {
            var accounts = _accountRepository.GetAll(idUser);
            var accountsDTO = accounts.ToListDTO();

            return accountsDTO;
        }

        public async Task<AccountDTO> GetAsyncIdAccount(int idAccount)
        {
            var account = await _accountRepository.GetIdAsync(idAccount);

            if (account == null) { return null; } // TODO : Сделать исключение

            var accountDTO = account.ToDTO();

            return accountDTO.AccountDTO;
        }
        public AccountDTO GetIdAccount(int idAccount)
        {
            var account = _accountRepository.GetId(idAccount);

            if (account == null) { return null; } // TODO : Сделать исключение

            var accountDTO = account.ToDTO();

            return accountDTO.AccountDTO;
        }

        public async Task<AccountDTO> GetAsyncAccountNumber(int? numberAccount)
        {
            var account = await _accountRepository.GetNumberAsync(numberAccount);

            if (account == null) { return null; } // TODO : Сделать исключение

            var accountDTO = account.ToDTO();

            return accountDTO.AccountDTO;
        }
        public AccountDTO GetAccountNumber(int? numberAccount)
        {
            var account = _accountRepository.GetNumber(numberAccount);

            if (account == null) { return null; } // TODO : Сделать исключение

            var accountDTO = account.ToDTO();

            return accountDTO.AccountDTO;
        }
    }
}

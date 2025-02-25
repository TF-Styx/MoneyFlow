using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.AccountCaseInterface;

namespace MoneyFlow.Application.Services.Realization
{
    public class AccountService : IAccountService
    {
        private readonly ICreateAccountUseCase _createAccountUseCase;
        private readonly IDeleteAccountUseCase _deleteAccountUseCase;
        private readonly IGetAccountUseCase _getAccountUseCase;
        private readonly IUpdateAccountUseCase _updateAccountUseCase;

        public AccountService(ICreateAccountUseCase createAccountUseCase, IDeleteAccountUseCase deleteAccountUseCase, IGetAccountUseCase getAccountUseCase, IUpdateAccountUseCase updateAccountUseCase)
        {
            _createAccountUseCase = createAccountUseCase;
            _deleteAccountUseCase = deleteAccountUseCase;
            _getAccountUseCase = getAccountUseCase;
            _updateAccountUseCase = updateAccountUseCase;
        }

        public async Task<(AccountDTO AccountDTO, string Message)> CreateAsyncAccount(int? numberAccount, int idUser, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance)
        {
            return await _createAccountUseCase.CreateAsyncAccount(numberAccount, idUser, bankDTO, accountTypeDTO, balance);
        }
        public (AccountDTO AccountDTO, string Message) CreateAccount(int? numberAccount, int idUser, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance)
        {
            return _createAccountUseCase.CreateAccount(numberAccount, idUser, bankDTO, accountTypeDTO, balance);
        }

        public async Task<List<AccountDTO>> GetAllAsyncAccount()
        {
            return await _getAccountUseCase.GetAllAsyncAccount();
        }
        public List<AccountDTO> GetAllAccount()
        {
            return _getAccountUseCase.GetAllAccount();
        }

        public async Task<AccountDTO> GetAsyncAccount(int idAccount)
        {
            return await _getAccountUseCase.GetAsyncIdAccount(idAccount);
        }
        public AccountDTO GetAccount(int idAccount)
        {
            return _getAccountUseCase.GetIdAccount(idAccount);
        }

        public async Task<AccountDTO> GetAsyncAccount(int? numberAccount)
        {
            return await _getAccountUseCase.GetAsyncAccountNumber(numberAccount);
        }
        public AccountDTO GetAccount(int? numberAccount)
        {
            return _getAccountUseCase.GetAccountNumber(numberAccount);
        }

        public async Task<int> UpdateAsyncAccount(int idAccount, int? numberAccount, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance)
        {
            return await _updateAccountUseCase.UpdateAsyncAccount(idAccount, numberAccount, bankDTO, accountTypeDTO, balance);
        }
        public int UpdateAccount(int idAccount, int? numberAccount, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance)
        {
            return _updateAccountUseCase.UpdateAccount(idAccount, numberAccount, bankDTO, accountTypeDTO, balance);
        }

        public async Task DeleteAsyncAccount(int idAccount)
        {
            await _deleteAccountUseCase.DeleteAsyncAccount(idAccount);
        }
        public void DeleteAccount(int idAccount)
        {
            _deleteAccountUseCase.DeleteAccount(idAccount);
        }
    }
}

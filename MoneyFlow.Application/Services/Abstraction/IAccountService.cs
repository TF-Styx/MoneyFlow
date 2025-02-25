using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.Services.Abstraction
{
    public interface IAccountService
    {
        Task<(AccountDTO AccountDTO, string Message)> CreateAsyncAccount(int? numberAccount, int idUser, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance);
        (AccountDTO AccountDTO, string Message) CreateAccount(int? numberAccount, int idUser, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance);

        Task<List<AccountDTO>> GetAllAsyncAccount();
        List<AccountDTO> GetAllAccount();

        Task<AccountDTO> GetAsyncAccount(int idAccount);
        AccountDTO GetAccount(int idAccount);
        
        Task<AccountDTO> GetAsyncAccount(int? numberAccount);
        AccountDTO GetAccount(int? numberAccount);

        Task<int> UpdateAsyncAccount(int idAccount, int? numberAccount, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance);
        int UpdateAccount(int idAccount, int? numberAccount, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance);

        Task DeleteAsyncAccount(int idAccount);
        void DeleteAccount(int idAccount);
    }
}
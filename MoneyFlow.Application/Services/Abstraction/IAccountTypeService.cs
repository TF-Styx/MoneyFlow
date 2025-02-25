using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.Services.Abstraction
{
    public interface IAccountTypeService
    {
        Task<(AccountTypeDTO AccountTypeDTO, string Message)> CreateAsyncAccountType(string accountTypeName);
        (AccountTypeDTO AccountTypeDTO, string Message) CreateAccountType(string accountTypeName);

        Task<List<AccountTypeDTO>> GetAllAsyncAccountType();
        List<AccountTypeDTO> GetAllAccountType();

        Task<AccountTypeDTO> GetAsyncAccountType(int idAccountType);
        AccountTypeDTO GetAccountType(int idAccountType);

        Task<AccountTypeDTO> GetAsyncAccountType(string accountTypeName);
        AccountTypeDTO GetAccountType(string accountTypeName);

        Task<int> UpdateAsyncAccountType(int idAccountType, string accountTypeName);
        int UpdateAccountType(int idAccountType, string accountTypeName);

        Task DeleteAsyncAccountType(int idAccountType);
        void DeleteBank(int idAccountType);
    }
}
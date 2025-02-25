using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.AccountTypeCaseInterfaces
{
    public interface IGetAccountTypeUseCase
    {
        Task<List<AccountTypeDTO>> GetAllAsyncAccountType();
        List<AccountTypeDTO> GetAllAccountType();

        Task<AccountTypeDTO> GetAsyncAccountType(int idAccountType);
        AccountTypeDTO GetAccountType(int idAccountType);

        Task<AccountTypeDTO> GetAsyncAccountType(string accountTypeName);
        AccountTypeDTO GetAccountType(string accountTypeName);
    }
}
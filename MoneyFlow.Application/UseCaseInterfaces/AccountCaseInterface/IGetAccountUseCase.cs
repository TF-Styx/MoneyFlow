using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.AccountCaseInterface
{
    public interface IGetAccountUseCase
    {
        Task<List<AccountDTO>> GetAllAsyncAccount(int idUser);
        List<AccountDTO> GetAllAccount(int idUser);

        Task<AccountDTO> GetAsyncIdAccount(int idAccount);
        AccountDTO GetIdAccount(int idAccount);

        Task<AccountDTO> GetAsyncAccountNumber(int? numberAccount);
        AccountDTO GetAccountNumber(int? numberAccount);
    }
}
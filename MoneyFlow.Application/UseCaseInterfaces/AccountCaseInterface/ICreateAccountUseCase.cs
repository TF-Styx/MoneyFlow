using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.AccountCaseInterface
{
    public interface ICreateAccountUseCase
    {
        Task<(AccountDTO AccountDTO, string Message)> CreateAsyncAccount(int? numberAccount, int idUser, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance);
        (AccountDTO AccountDTO, string Message) CreateAccount(int? numberAccount, int idUser, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance);
    }
}
using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.AccountTypeCaseInterfaces
{
    public interface ICreateAccountTypeUseCase
    {
        Task<(AccountTypeDTO AccountTypeDTO, string Message)> CreateAsyncAccountType(string accountTypeName);
        (AccountTypeDTO AccountTypeDTO, string Message) CreateAccountType(string accountTypeName);
    }
}
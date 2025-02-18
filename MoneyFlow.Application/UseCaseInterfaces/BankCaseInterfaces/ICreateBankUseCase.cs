using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces
{
    public interface ICreateBankUseCase
    {
        Task<(BankDTO BankDTO, string Message)> CreateAsyncBank(string bankName);
        (BankDTO BankDTO, string Message) CreateBank(string bankName);
    }
}
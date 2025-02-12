namespace MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces
{
    public interface IUpdateBankUseCase
    {
        Task<int> UpdateBank(int idBank, string bankName);
    }
}
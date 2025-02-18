namespace MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces
{
    public interface IUpdateBankUseCase
    {
        Task<int> UpdateAsyncBank(int idBank, string bankName);
        int UpdateBank(int idBank, string bankName);
    }
}
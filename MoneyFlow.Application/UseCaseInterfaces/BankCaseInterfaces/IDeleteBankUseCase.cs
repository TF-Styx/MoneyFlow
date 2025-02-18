namespace MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces
{
    public interface IDeleteBankUseCase
    {
        Task DeleteAsyncBank(int idBank);
        void DeleteBank(int idBank);
    }
}
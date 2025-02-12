namespace MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces
{
    public interface IDeleteBankUseCase
    {
        Task DeleteBank(int idBank);
    }
}
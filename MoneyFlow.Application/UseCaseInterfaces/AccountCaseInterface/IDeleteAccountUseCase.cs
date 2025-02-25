namespace MoneyFlow.Application.UseCaseInterfaces.AccountCaseInterface
{
    public interface IDeleteAccountUseCase
    {
        Task DeleteAsyncAccount(int idAccount);
        void DeleteAccount(int idAccount);
    }
}
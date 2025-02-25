namespace MoneyFlow.Application.UseCaseInterfaces.AccountTypeCaseInterfaces
{
    public interface IDeleteAccountTypeUseCase
    {
        Task DeleteAsyncAccountType(int idAccountType);
        void DeleteAccountType(int idAccountType);
    }
}
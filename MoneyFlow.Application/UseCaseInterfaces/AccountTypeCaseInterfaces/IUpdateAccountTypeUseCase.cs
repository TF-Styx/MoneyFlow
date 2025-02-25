namespace MoneyFlow.Application.UseCaseInterfaces.AccountTypeCaseInterfaces
{
    public interface IUpdateAccountTypeUseCase
    {
        Task<int> UpdateAsyncAccountType(int idAccountType, string accountTypeName);
        int UpdateAccountType(int idAccountType, string accountTypeName);
    }
}
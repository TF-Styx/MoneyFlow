namespace MoneyFlow.Application.UseCaseInterfaces.FinancialRecordCaseInterfaces
{
    public interface IDeleteFinancialRecordUseCase
    {
        Task DeleteAsync(int idFinancialRecord);
        void Delete(int idFinancialRecord);
    }
}
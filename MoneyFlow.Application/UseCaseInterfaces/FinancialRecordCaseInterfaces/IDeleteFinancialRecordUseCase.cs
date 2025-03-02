namespace MoneyFlow.Application.UseCaseInterfaces.FinancialRecordCaseInterfaces
{
    public interface IDeleteFinancialRecordUseCase
    {
        Task DeleteAsyncFinancialRecord(int idFinancialRecord);
        void DeleteFinancialRecord(int idFinancialRecord);
    }
}
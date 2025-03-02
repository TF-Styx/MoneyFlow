namespace MoneyFlow.Application.UseCaseInterfaces.FinancialRecordCaseInterfaces
{
    public interface IUpdateFinancialRecordUseCase
    {
        Task<int> UpdateAsyncFinancialRecord(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);
        int UpdateFinancialRecord(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);
    }
}
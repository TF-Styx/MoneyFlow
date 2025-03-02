using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.FinancialRecordCaseInterfaces
{
    public interface ICreateFinancialRecordUseCase
    {
        Task<(FinancialRecordDTO FinancialRecordDTO, string Message)> CreateAsyncFinancialRecord(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);
        (FinancialRecordDTO FinancialRecordDTO, string Message) CreateFinancialRecord(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);
    }
}
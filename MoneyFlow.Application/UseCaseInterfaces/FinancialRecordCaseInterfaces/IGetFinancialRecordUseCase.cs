using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.FinancialRecordCaseInterfaces
{
    public interface IGetFinancialRecordUseCase
    {
        Task<List<FinancialRecordDTO>> GetAllAsyncFinancialRecord();
        List<FinancialRecordDTO> GetAllFinancialRecord();

        Task<FinancialRecordDTO> GetAsyncFinancialRecord(int idFinancialRecord);
        FinancialRecordDTO GetFinancialRecord(int idFinancialRecord);

        Task<FinancialRecordDTO> GetAsyncFinancialRecord(string recordName);
        FinancialRecordDTO GetFinancialRecord(string recordName);
    }
}
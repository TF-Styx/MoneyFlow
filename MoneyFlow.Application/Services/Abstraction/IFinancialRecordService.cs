using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.Services.Abstraction
{
    public interface IFinancialRecordService
    {
        Task<(FinancialRecordDTO FinancialRecordDTO, string Message)> CreateAsyncFinancialRecord(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);
        (FinancialRecordDTO FinancialRecordDTO, string Message) CreateFinancialRecord(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);

        Task<List<FinancialRecordDTO>> GetAllAsyncFinancialRecord();
        List<FinancialRecordDTO> GetAllFinancialRecord();

        Task<FinancialRecordDTO> GetAsyncFinancialRecord(int idFinancialRecord);
        FinancialRecordDTO GetFinancialRecord(int idFinancialRecord);

        Task<FinancialRecordDTO> GetAsyncFinancialRecord(string recordName);
        FinancialRecordDTO GetFinancialRecord(string recordName);

        Task<int> UpdateAsyncFinancialRecord(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);
        int UpdateFinancialRecord(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date);

        Task DeleteAsyncFinancialRecord(int idFinancialRecord);
        void DeleteFinancialRecord(int idFinancialRecord);
    }
}
using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.FinancialRecordCaseInterfaces;

namespace MoneyFlow.Application.Services.Realization
{
    public class FinancialRecordService : IFinancialRecordService
    {
        private readonly ICreateFinancialRecordUseCase _createFinancialRecordUseCase;
        private readonly IDeleteFinancialRecordUseCase _deleteFinancialRecordUseCase;
        private readonly IGetFinancialRecordUseCase _getFinancialRecordUseCase;
        private readonly IUpdateFinancialRecordUseCase _updateFinancialRecordUseCase;

        public FinancialRecordService(ICreateFinancialRecordUseCase createFinancialRecordUseCase, IDeleteFinancialRecordUseCase deleteFinancialRecordUseCase, IGetFinancialRecordUseCase getFinancialRecordUseCase, IUpdateFinancialRecordUseCase updateFinancialRecordUseCase)
        {
            _createFinancialRecordUseCase = createFinancialRecordUseCase;
            _deleteFinancialRecordUseCase = deleteFinancialRecordUseCase;
            _getFinancialRecordUseCase = getFinancialRecordUseCase;
            _updateFinancialRecordUseCase = updateFinancialRecordUseCase;
        }

        public async Task<(FinancialRecordDTO FinancialRecordDTO, string Message)> CreateAsyncFinancialRecord(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            return await _createFinancialRecordUseCase.CreateAsyncFinancialRecord(recordName, amount, description, idTransactionType, idUser, idCategory, idAccount, date);
        }
        public (FinancialRecordDTO FinancialRecordDTO, string Message) CreateFinancialRecord(string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            return _createFinancialRecordUseCase.CreateFinancialRecord(recordName, amount, description, idTransactionType, idUser, idCategory, idAccount, date);
        }

        public async Task<List<FinancialRecordDTO>> GetAllAsyncFinancialRecord()
        {
            return await _getFinancialRecordUseCase.GetAllAsyncFinancialRecord();
        }
        public List<FinancialRecordDTO> GetAllFinancialRecord()
        {
            return _getFinancialRecordUseCase.GetAllFinancialRecord();
        }

        public async Task<FinancialRecordDTO> GetAsyncFinancialRecord(int idFinancialRecord)
        {
            return await _getFinancialRecordUseCase.GetAsyncFinancialRecord(idFinancialRecord);
        }
        public FinancialRecordDTO GetFinancialRecord(int idFinancialRecord)
        {
            return _getFinancialRecordUseCase.GetFinancialRecord(idFinancialRecord);
        }

        public async Task<FinancialRecordDTO> GetAsyncFinancialRecord(string recordName)
        {
            return await _getFinancialRecordUseCase.GetAsyncFinancialRecord(recordName);
        }
        public FinancialRecordDTO GetFinancialRecord(string recordName)
        {
            return _getFinancialRecordUseCase.GetFinancialRecord(recordName);
        }

        public async Task<int> UpdateAsyncFinancialRecord(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            return await _updateFinancialRecordUseCase.UpdateAsyncFinancialRecord(idFinancialRecord, recordName, amount, description, idTransactionType, idUser, idCategory, idAccount, date);
        }
        public int UpdateFinancialRecord(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            return _updateFinancialRecordUseCase.UpdateFinancialRecord(idFinancialRecord, recordName, amount, description, idTransactionType, idUser, idCategory, idAccount, date);
        }

        public async Task DeleteAsyncFinancialRecord(int idFinancialRecord)
        {
            await _deleteFinancialRecordUseCase.DeleteAsyncFinancialRecord(idFinancialRecord);
        }
        public void DeleteFinancialRecord(int idFinancialRecord)
        {
            _deleteFinancialRecordUseCase.DeleteFinancialRecord(idFinancialRecord);
        }
    }
}

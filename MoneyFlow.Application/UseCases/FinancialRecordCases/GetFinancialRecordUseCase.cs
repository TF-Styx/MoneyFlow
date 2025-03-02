using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.FinancialRecordCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.FinancialRecordCases
{
    public class GetFinancialRecordUseCase : IGetFinancialRecordUseCase
    {
        private readonly IFinancialRecordRepository _financialRecordRepository;

        public GetFinancialRecordUseCase(IFinancialRecordRepository financialRecordRepository)
        {
            _financialRecordRepository = financialRecordRepository;
        }

        public async Task<List<FinancialRecordDTO>> GetAllAsyncFinancialRecord()
        {
            var financialRecords = await _financialRecordRepository.GetAllAsync();
            var financialRecordsDTO = financialRecords.ToListDTO();

            return financialRecordsDTO;
        }
        public List<FinancialRecordDTO> GetAllFinancialRecord()
        {
            var financialRecords = _financialRecordRepository.GetAll();
            var financialRecordsDTO = financialRecords.ToListDTO();

            return financialRecordsDTO;
        }

        public async Task<FinancialRecordDTO> GetAsyncFinancialRecord(int idFinancialRecord)
        {
            var financialRecord = await _financialRecordRepository.GetAsync(idFinancialRecord);

            if (financialRecord == null) { return null; }

            var financialRecordDTO = financialRecord.ToDTO();

            return financialRecordDTO.FinancialRecordDTO;
        }
        public FinancialRecordDTO GetFinancialRecord(int idFinancialRecord)
        {
            var financialRecord = _financialRecordRepository.Get(idFinancialRecord);

            if (financialRecord == null) { return null; }

            var financialRecordDTO = financialRecord.ToDTO();

            return financialRecordDTO.FinancialRecordDTO;
        }

        public async Task<FinancialRecordDTO> GetAsyncFinancialRecord(string recordName)
        {
            var financialRecord = await _financialRecordRepository.GetAsync(recordName);

            if (financialRecord == null) { return null; }

            var financialRecordDTO = financialRecord.ToDTO();

            return financialRecordDTO.FinancialRecordDTO;
        }
        public FinancialRecordDTO GetFinancialRecord(string recordName)
        {
            var financialRecord = _financialRecordRepository.Get(recordName);

            if (financialRecord == null) { return null; }

            var financialRecordDTO = financialRecord.ToDTO();

            return financialRecordDTO.FinancialRecordDTO;
        }
    }
}

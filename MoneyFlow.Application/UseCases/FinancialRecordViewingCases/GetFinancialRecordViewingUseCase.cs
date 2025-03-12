using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.FinancialRecordViewingInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.FinancialRecordViewingCases
{
    public class GetFinancialRecordViewingUseCase : IGetFinancialRecordViewingUseCase
    {
        private readonly IFinancialRecordRepository _financialRecordRepository;

        public GetFinancialRecordViewingUseCase(IFinancialRecordRepository financialRecordRepository)
        {
            _financialRecordRepository = financialRecordRepository;
        }

        public async Task<List<FinancialRecordViewingDTO>> GetAllAsyncFinancialRecordViewing(int idUser)
        {
            var financialRecords = await _financialRecordRepository.GetAllViewingAsync(idUser);
            var financialRecordsDTO = financialRecords.ToListDTO();

            return financialRecordsDTO;
        }
        public List<FinancialRecordViewingDTO> GetAllFinancialRecordViewing(int idUser)
        {
            var financialRecords = _financialRecordRepository.GetAllViewing(idUser);
            var financialRecordsDTO = financialRecords.ToListDTO();

            return financialRecordsDTO;
        }

        public async Task<FinancialRecordViewingDTO> GetByIdAsync(int idUser, int idFinancialRecord, int idCategory, int idSubcategory)
        {
            var financialRecord = await _financialRecordRepository.GetByIdAsync(idUser, idFinancialRecord, idCategory, idSubcategory);
            var financialRecordDTO = financialRecord.ToDTO().FinancialRecordViewingDTO;

            return financialRecordDTO;
        }
        public FinancialRecordViewingDTO GetById(int idUser, int idFinancialRecord, int idCategory, int idSubcategory)
        {
            var financialRecord = _financialRecordRepository.GetById(idUser, idFinancialRecord, idCategory, idSubcategory);
            var financialRecordDTO = financialRecord.ToDTO().FinancialRecordViewingDTO;

            return financialRecordDTO;
        }
    }
}

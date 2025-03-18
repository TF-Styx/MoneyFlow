using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.FinancialRecordViewingInterfaces
{
    public interface IGetFinancialRecordViewingUseCase
    {
        Task<List<FinancialRecordViewingDTO>> GetAllAsyncFinancialRecordViewing(int idUser, FinancialRecordFilterDTO filter);
        List<FinancialRecordViewingDTO> GetAllFinancialRecordViewing(int idUser);

        Task<FinancialRecordViewingDTO> GetByIdAsync(int idUser, int idFinancialRecord, int idCategory, int idSubcategory);
        FinancialRecordViewingDTO GetById(int idUser, int idFinancialRecord, int idCategory, int idSubcategory);
    }
}